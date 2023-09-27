(function() {
    var $doc = document.querySelector('html');
    var $btn = document.querySelector('.header-nav__hamburgger');

    $btn.addEventListener('click', function() {
        $doc.classList.toggle('menu-opened');
    })
})()