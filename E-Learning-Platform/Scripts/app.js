

    function toggleDropdown() {
                var dropdown = document.getElementById("myDropdown");
    dropdown.style.display = dropdown.style.display === "block" ? "none" : "block";
            }


    window.onclick = function (event) {
                if (!event.target.matches('.dropdown button')) {
                    var dropdown = document.getElementById("myDropdown");
    if (dropdown.style.display === "block") {
        dropdown.style.display = "none";
                    }
                }
            }


