document.addEventListener('DOMContentLoaded', function () {
    const themeToggle = document.getElementById('themeToggle');
    const themeIcon = document.getElementById('themeIcon');
    const currentTheme = localStorage.getItem('theme') || 'light';
    
    // Function to update link colors based on the current theme (if needed)
    const updateThemeColors = () => {
        const isDark = document.documentElement.getAttribute('data-bs-theme') === 'dark';
        document.querySelectorAll('.nav-link').forEach(link => {
            link.classList.toggle('text-dark', !isDark);
            link.classList.toggle('text-light', isDark);
        });
    };

    // Initialize the theme based on saved preference
    if (currentTheme === 'dark') {
        document.documentElement.setAttribute('data-bs-theme', 'dark');
        themeIcon.className = 'fas fa-sun';  // Sun icon for dark theme
    } else {
        themeIcon.className = 'fas fa-moon'; // Moon icon for light theme
    }

    // Toggle theme on icon click
    themeToggle.addEventListener('click', function () {
        const isDark = document.documentElement.getAttribute('data-bs-theme') === 'dark';

        if (isDark) {
            // Switch to light theme
            document.documentElement.setAttribute('data-bs-theme', 'light');
            themeIcon.className = 'fas fa-moon';
            localStorage.setItem('theme', 'light');
        } else {
            // Switch to dark theme
            document.documentElement.setAttribute('data-bs-theme', 'dark');
            themeIcon.className = 'fas fa-sun';
            localStorage.setItem('theme', 'dark');
        }

        updateThemeColors();// Update link colors
    });

    // Set initial link colors based on the current theme
    updateThemeColors();
});