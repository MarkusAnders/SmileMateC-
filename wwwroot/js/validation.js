// Маска для телефона
$('#phone').mask("+7(999) 999-99-99");

// Маска для ввода даты
document.addEventListener("DOMContentLoaded", function() {
    var dateInput = document.getElementById("birthdate");
    var today = new Date().toISOString().split('T')[0];
    dateInput.setAttribute('max', today);
});

// Маска для почты
function validateEmail() {
    const emailInput = document.getElementById('email');
    const errorMessage = document.getElementById('error-message');
    const submitButton = document.getElementById('submit-button');
    const validDomains = ['@mail.ru', '@yandex.ru', '@gmail.com', '@yahoo.com'];
    const emailValue = emailInput.value;
    
     // Регулярное выражение для проверки, что перед доменом есть хотя бы один символ
    const isValid = validDomains.some(domain => {
        const regex = new RegExp(`^[^\\s@]+${domain}$`);
        return regex.test(emailValue);
    });

    if (!isValid) {
        errorMessage.textContent = 'Email должен заканчиваться на @mail.ru, @yandex.ru, @gmail.com, @yahoo.com.';
        errorMessage.style.display = 'inline';
        submitButton.disabled = true;
    } else {
        errorMessage.textContent = '';
        errorMessage.style.display = 'none';
        submitButton.disabled = false;
    }
}

 // Функция для проверки выбранной радиокнопки
function checkRadioSelection() {
    const radioButtons = document.getElementsByName('rb');
    let selected = false;
    for (const radioButton of radioButtons) {
        if (radioButton.checked) {
            selected = true;
            break;
        }
    }
    const errorMessage = document.getElementById('error-message-reg');
    if (selected) {
        errorMessage.style.display = 'none';
    }
}

// Обработчик события для кнопки "Зарегистрировать"
document.getElementById('submitBtn').addEventListener('click', function(event) {
    const radioButtons = document.getElementsByName('rb');
    let selected = false;
    for (const radioButton of radioButtons) {
        if (radioButton.checked) {
            selected = true;
            break;
        }
    }
    const errorMessage = document.getElementById('error-message-reg');
    if (!selected) {
        event.preventDefault();
        errorMessage.style.display = 'block';
    } else {
        errorMessage.style.display = 'none';
    }
});

// Добавление обработчика события для изменения состояния радиокнопок
const radioButtons = document.getElementsByName('rb');
for (const radioButton of radioButtons) {
    radioButton.addEventListener('change', checkRadioSelection);
}

// проверка на пользователя и неверные данные
document.getElementById("auth-form").addEventListener("submit", function (event) {
    event.preventDefault();  // Предотвращает перезагрузку страницы

    var form = event.target;
    var data = new FormData(form);  // Собирает данные формы

    fetch(form.action, {
        method: "POST",
        body: data,
        headers: {
            'Accept': 'application/json'  // Указывает, что клиент ожидает JSON-ответ
        }
    })
    .then(response => response.json())  // Обработка JSON-ответа
    .then(data => {
        if (data.success) {
            window.location.href = data.redirectUrl;  // Перенаправление при успешной авторизации
        } else {
            document.getElementById("auth-error").innerText = data.message;  // Отображение ошибки
        }
    })
    .catch(error => {
        console.error("Error:", error);
        document.getElementById("auth-error").innerText = "Произошла ошибка. Попробуйте еще раз.";
    });
});