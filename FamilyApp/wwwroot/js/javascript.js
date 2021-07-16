function hideCookies() {
    var info = document.getElementById("cookieInfo");
    document.cookie = "acceptedCookies=true; path=/";
    info.style.display = "none";
}

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

function checkIfCookiesAccepted() {
    var cookie = getCookie("acceptedCookies");
    if (cookie == 'true')
        return true;
    return false;
}

function checkIfDarkmodeEnabled() {
    var cookie = getCookie("darkmodeEnaled");
    if (cookie == 'true')
        return true;
    return false;
}

function onStart() {
    if (checkIfCookiesAccepted()) {
        var info = document.getElementById("cookieInfo");
        info.style.display = "none";
    }
    if (checkIfDarkmodeEnabled()) {
        toggleDarkMode();
    }
}

function toggleDarkMode() {
    if ($("body").hasClass("dark")) {
        document.cookie = "darkmodeEnaled=false; path=/";
        $("body").removeClass("dark");
        $("div").removeClass("dark");
        $("nav").addClass("bg-light");
        $("nav").addClass("navbar-light");
        $("nav").removeClass("bg-dark");
        $("nav").removeClass("navbar-dark");
        $("a").removeClass("dark");
        $("tr").removeClass("dark");
        $("ul").removeClass("dark");
        $("input").removeClass("dark");
        $("textarea").removeClass("dark");
        $("select").removeClass("dark");
        $(".btn-close").removeClass("btn-close-white");
    } else {
        document.cookie = "darkmodeEnaled=true; path=/";
        $("body").addClass("dark");
        $("div").addClass("dark");
        $("nav").removeClass("bg-light");
        $("nav").removeClass("navbar-light");
        $("nav").addClass("bg-dark");
        $("nav").addClass("navbar-dark");
        $("a").addClass("dark");
        $("tr").addClass("dark");
        $("ul").addClass("dark");
        $("input").addClass("dark");
        $("textarea").addClass("dark");
        $("select").addClass("dark");
        $(".btn-close").addClass("btn-close-white");
    }
}

function clickById(id) {
    var element = document.getElementById(id);
    element.click();
}

function changeQuery(query = '', value = '') {
    var url = window.location.search;
    var params = new URLSearchParams(url);
    params.set(query, value);
    window.location.search = params.toString();
}