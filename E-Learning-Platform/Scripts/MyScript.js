let lastScroll = 0;
const navbar = document.getElementById('mainNavbar');
window.addEventListener('scroll', function () {
    const currentScroll = window.pageYOffset;
    if (currentScroll < lastScroll) {
        // Scrolling up
        navbar.classList.add('scrolled-up');
    } else {
        // Scrolling down
        navbar.classList.remove('scrolled-up');
    }
    lastScroll = currentScroll;
});