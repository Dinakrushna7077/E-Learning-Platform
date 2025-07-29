let lastScroll = 0;
const navbar = document.getElementById("mainNavbar");

window.addEventListener("scroll", function () {
    const currentScroll = window.scrollY;

    if (currentScroll < lastScroll) {
        navbar.classList.add("scrolled-up");
    } else {
        navbar.classList.remove("scrolled-up");
    }
    lastScroll = currentScroll;
});
