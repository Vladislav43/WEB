$(document).ready(function () {
    // Привітання
    greetUser();

    // Функція привітання
    function greetUser() {
        var greetings = [
            "Hello!",
            "Hello, how can I help you?",
        ];

        var randomGreeting = greetings[Math.floor(Math.random() * greetings.length)];

        $('#chat-container').append('<p><strong>Wolfram Alpha:</strong> ' + randomGreeting + '</p>');
    }

    $('#chat-form').submit(function (e) {
        e.preventDefault();

        var userQuestion = $('#user-input').val();

        if (userQuestion.trim() === '') {
            return;
        }

        $('#chat-container').append('<p><strong>USER:</strong> ' + userQuestion + '</p>');

        $.ajax({
            url: 'https://api.wolframalpha.com/v2/query',
            data: {
                input: userQuestion,
                appid: 'AK85W3-WTY44X4AQJ',
                output: 'json'
            },
            dataType: 'jsonp',
            success: function (response) {
                var answer = response.queryresult.pods[1].subpods[0].plaintext;

                $('#chat-container').append('<p><strong>Wolfram Alpha:</strong> ' + answer + '</p>');

                $('#user-input').val('');
            }
        });
    });
});
