
// Показывание пароля на авторизации
function togglePassword() {
    var passwordInput = document.querySelector('.input-password');
    var eyeIcon = document.getElementById('eye-icon');
    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        eyeIcon.classList.add('show');
    } else {
        passwordInput.type = 'password';
        eyeIcon.classList.remove('show');
    }
}

// После истечения времени приема +1 час закрывать прием

function parseDateTime(date, time) {
    const [day, month, year] = date.split('.').map(Number); // Разделяем дату на день, месяц и год, преобразуем в числа
    const [hours, minutes] = time.split(':').map(Number); // Разделяем время на часы и минуты, преобразуем в числа
    return new Date(year, month - 1, day, hours, minutes); // Создаем и возвращаем объект Date (месяцы в объекте Date считаются с 0)
}


function addPassedClass() {
    const rows = document.querySelectorAll('.reception-row'); // Находим все элементы с классом .reception-row
    const now = new Date(); // Получаем текущее время

    rows.forEach(row => {
        const date = row.getAttribute('data-date'); // Получаем дату из атрибута data-date
        const time = row.getAttribute('data-time'); // Получаем время из атрибута data-time
        const rowTime = parseDateTime(date, time); // Парсим дату и время в объект Date

        if (now > rowTime) { // Проверяем, прошло ли текущее время относительно rowTime
            const diffInMs = now - rowTime; // Разница во времени в миллисекундах
            const diffInHours = diffInMs / (1000 * 60 * 60); // Преобразуем разницу в часы

            if (diffInHours >= 1) { // Если прошло больше часа
                row.classList.add('passed'); // Добавляем класс passed
            } else {
                const timeLeft = (1 - diffInHours) * 60 * 60 * 1000; // Вычисляем оставшееся время до часа в миллисекундах
                setTimeout(() => {
                    row.classList.add('passed'); // Планируем добавление класса passed через оставшееся время
                }, timeLeft);
            }
        }
    });
}

// Проверка каждые 10 минут
setInterval(addPassedClass, 10 * 60 * 1000);

// Проверка при загрузке страницы
document.addEventListener('DOMContentLoaded', addPassedClass);


// Запрет на нажатие "создать посещение" пока время не наступит
document.addEventListener("DOMContentLoaded", function() {
    var rows = document.querySelectorAll(".reception-row");

    rows.forEach(function(row) {
        var receptionDateTime = new Date(row.dataset.datetime);
        var currentDateTime = new Date();

        if (currentDateTime < receptionDateTime) {
            var createVisitButton = row.querySelector(".table-btn--history");
            if (createVisitButton) {
                createVisitButton.disabled = true;
                createVisitButton.classList.add("disabled");
            }
        }
    });
});