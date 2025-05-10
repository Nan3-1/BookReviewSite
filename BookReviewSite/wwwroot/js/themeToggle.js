document.addEventListener('DOMContentLoaded', function () {
    const themeToggle = document.getElementById('themeToggle');
    const themeIcon = document.getElementById('themeIcon');
    const currentTheme = localStorage.getItem('theme') || 'light';

    // Функция за обновяване на цветовете (ако е необходима)
    const updateThemeColors = () => {
        const isDark = document.documentElement.getAttribute('data-bs-theme') === 'dark';
        document.querySelectorAll('.nav-link').forEach(link => {
            link.classList.toggle('text-dark', !isDark);
            link.classList.toggle('text-light', isDark);
        });
    };

    // Инициализация на темата
    if (currentTheme === 'dark') {
        document.documentElement.setAttribute('data-bs-theme', 'dark');
        themeIcon.className = 'fas fa-sun'; // Слънце за тъмна тема
    } else {
        themeIcon.className = 'fas fa-moon'; // Луна за светла тема
    }

    // Смяна на темата при клик на иконката
    themeToggle.addEventListener('click', function () {
        const isDark = document.documentElement.getAttribute('data-bs-theme') === 'dark';

        if (isDark) {
            // Превключване към светла тема
            document.documentElement.setAttribute('data-bs-theme', 'light');
            themeIcon.className = 'fas fa-moon';
            localStorage.setItem('theme', 'light');
        } else {
            // Превключване към тъмна тема
            document.documentElement.setAttribute('data-bs-theme', 'dark');
            themeIcon.className = 'fas fa-sun';
            localStorage.setItem('theme', 'dark');
        }

        updateThemeColors(); // Обновяване на цветовете (ако се използва)
    });

    // Инициализиране на цветовете
    updateThemeColors();
});