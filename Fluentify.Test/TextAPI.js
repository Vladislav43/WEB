// ϳ��������� ���������� ������� �� ������������ ����������
const assert = require('assert');
const { JSDOM } = require('jsdom');

// ������������ HTML-������� � ����� ���-����
const html = `
<!DOCTYPE html>
<html>
<head>
    <title>Chat Bot</title>
    <style>
        /* ���� ��� ���-���� */
    </style>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</head>
<body>
    <h1>Chat Bot</h1>

    <div id="chat-container"></div>

    <form id="chat-form">
        <input type="text" id="user-input" placeholder="������� ���� �������" />
        <button type="submit">��������</button>
    </form>

    <script src="/js/script_2.js"></script>
</body>
</html>
`;

// ���� �����
describe('Chat Bot', function () {
    let window, document, $;

    // ����� ������ ������ ��������� ���� ��������� DOM
    beforeEach(function () {
        const dom = new JSDOM(html, { runScripts: 'dangerously' });
        window = dom.window;
        document = window.document;
        $ = window.jQuery;
    });

    it('should greet the user', function () {
        // ��������� ������� greetUser()
        window.greetUser();

        // ����������, �� � ��������� ���� �'������ ����� ���������
        const chatContainer = document.getElementById('chat-container');
        const greetings = chatContainer.getElementsByTagName('p');
        assert.strictEqual(greetings.length, 1);

        // ����������, �� ����� ��������� ������ ���������� �����
        const greetingText = greetings[0].textContent;
        assert(greetingText.includes('Wolfram Alpha:'));
    });

    it('should send user question and receive answer', function (done) {
        // ������������ ���������� ��'��� Ajax ��� ����������
        window.$.ajax = function (options) {
            // ����������, �� ��������� ���������� AJAX-�����
            assert.strictEqual(options.url, 'https://api.wolframalpha.com/v2/query');
            assert.strictEqual(options.data.input, 'Test question');
            assert.strictEqual(options.data.appid, 'AK85W3-WTY44X4AQJ');
            assert.strictEqual(options.data.output, 'json');
            assert.strictEqual(options.dataType, 'jsonp');

            // ��������� ��������� ������� � ������������ ��������� ������
            const response = {
                queryresult: {
                    pods: [
                        {
                            subpods: [{ plaintext: 'Test answer' }]
                        }
                    ]
                }
            };

            // ��������� ������� ����� � ���������� ��������
            options.success(response);
        };

        // ������������ �������� � ���� ����� �����������
        const userInput = document.getElementById('user-input');
        userInput.value = 'Test question';

        // ³���������� �����
        const chatForm = document.getElementById('chat-form');
        chatForm.dispatchEvent(new window.Event('submit'));

        // ��������� �������� ���������� ����� �� ���������� ����������� ��������
        setTimeout(function () {
            // ����������, �� � ��������� ���� �'������ ����� � �������� �����������
            const chatContainer = document.getElementById('chat-container');
            const userMessages = chatContainer.getElementsByTagName('p');
            assert.strictEqual(userMessages.length, 1);
            assert(userMessages[0].textContent.includes('USER:'));
            assert(userMessages[0].textContent.includes('Test question'));

            // ����������, �� � ��������� ���� �'������ ����� � ��������
            const botMessages = chatContainer.getElementsByTagName('p');
            assert.strictEqual(botMessages.length, 2);
            assert(botMessages[1].textContent.includes('Wolfram Alpha:'));
            assert(botMessages[1].textContent.includes('Test answer'));

            // ����������, �� ���� ����� ����������� ���� �������
            assert.strictEqual(userInput.value, '');

            done();
        }, 100);
    });
});
