// Підключення необхідних бібліотек та налаштування тестування
const assert = require('assert');
const { JSDOM } = require('jsdom');

// Завантаження HTML-сторінки з кодом чат-бота
const html = `
<!DOCTYPE html>
<html>
<head>
    <title>Chat Bot</title>
    <style>
        /* Стилі для чат-бота */
    </style>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</head>
<body>
    <h1>Chat Bot</h1>

    <div id="chat-container"></div>

    <form id="chat-form">
        <input type="text" id="user-input" placeholder="Задайте ваше питання" />
        <button type="submit">Надіслати</button>
    </form>

    <script src="/js/script_2.js"></script>
</body>
</html>
`;

// Опис тесту
describe('Chat Bot', function () {
    let window, document, $;

    // Перед кожним тестом запускаємо нову інстанцію DOM
    beforeEach(function () {
        const dom = new JSDOM(html, { runScripts: 'dangerously' });
        window = dom.window;
        document = window.document;
        $ = window.jQuery;
    });

    it('should greet the user', function () {
        // Викликаємо функцію greetUser()
        window.greetUser();

        // Перевіряємо, що в контейнері чату з'явився рядок привітання
        const chatContainer = document.getElementById('chat-container');
        const greetings = chatContainer.getElementsByTagName('p');
        assert.strictEqual(greetings.length, 1);

        // Перевіряємо, що рядок привітання містить правильний текст
        const greetingText = greetings[0].textContent;
        assert(greetingText.includes('Wolfram Alpha:'));
    });

    it('should send user question and receive answer', function (done) {
        // Встановлюємо підроблений об'єкт Ajax для тестування
        window.$.ajax = function (options) {
            // Перевіряємо, що правильно виконується AJAX-запит
            assert.strictEqual(options.url, 'https://api.wolframalpha.com/v2/query');
            assert.strictEqual(options.data.input, 'Test question');
            assert.strictEqual(options.data.appid, 'AK85W3-WTY44X4AQJ');
            assert.strictEqual(options.data.output, 'json');
            assert.strictEqual(options.dataType, 'jsonp');

            // Створюємо підроблену відповідь з встановленим значенням відповіді
            const response = {
                queryresult: {
                    pods: [
                        {
                            subpods: [{ plaintext: 'Test answer' }]
                        }
                    ]
                }
            };

            // Викликаємо функцію успіху з підробленою відповіддю
            options.success(response);
        };

        // Встановлюємо значення в поле вводу користувача
        const userInput = document.getElementById('user-input');
        userInput.value = 'Test question';

        // Відправляємо форму
        const chatForm = document.getElementById('chat-form');
        chatForm.dispatchEvent(new window.Event('submit'));

        // Затримуємо перевірку результату тесту до завершення асинхронних операцій
        setTimeout(function () {
            // Перевіряємо, що в контейнері чату з'явився рядок з питанням користувача
            const chatContainer = document.getElementById('chat-container');
            const userMessages = chatContainer.getElementsByTagName('p');
            assert.strictEqual(userMessages.length, 1);
            assert(userMessages[0].textContent.includes('USER:'));
            assert(userMessages[0].textContent.includes('Test question'));

            // Перевіряємо, що в контейнері чату з'явився рядок з відповіддю
            const botMessages = chatContainer.getElementsByTagName('p');
            assert.strictEqual(botMessages.length, 2);
            assert(botMessages[1].textContent.includes('Wolfram Alpha:'));
            assert(botMessages[1].textContent.includes('Test answer'));

            // Перевіряємо, що поле вводу користувача було очищено
            assert.strictEqual(userInput.value, '');

            done();
        }, 100);
    });
});
