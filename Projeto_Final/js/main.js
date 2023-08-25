(function() {
    var $body = document.querySelector('body');
    $body.classList.remove('no-js');
    $body.classList.add('js');

    var menu = new Menu({
        container: '.header__nav',
        toggleBtn: '.header__btnMenu',
        widthEnable: 1024
    });

    var carouselImgs = new Carousel({
        container: '.laptop-slider .slideshow',
        itens: 'figure',
        btnPrev: '.btnPrev',
        btnNext: '.btnNext'
    });

    var corouselQuotes = new Quote({
        container: '.quote-container .quote-slideshow',
        itens: 'figure',
        btnPrev: '.btnPrev',
        btnNext: '.btnNext'
    });
})()