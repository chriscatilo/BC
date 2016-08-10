(function() {
    if (location.protocol !== "https:") {
        window.location.href = "/HttpsOnly.html";
    }
})();