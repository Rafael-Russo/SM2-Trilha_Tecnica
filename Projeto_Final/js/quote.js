function Quote(config) {
    this.container = (typeof config.container == 'string') ? document.querySelector(config.container) : config.container;
    this.itens = (typeof config.itens == 'string') ? this.container.querySelectorAll(config.itens) : config.itens;
    this.btnPrev = (typeof config.btnPrev == 'string') ? this.container.querySelector(config.btnPrev) : config.btnPrev;
    this.btnNext = (typeof config.btnNext == 'string') ? this.container.querySelector(config.btnNext) : config.btnNext;

    var _this = this;
    var _currentQuote = 0;

    init();

    function init() {
        var _show = _this.container.querySelectorAll('.show');

        Array.prototype.forEach.call(_show, function(sh) {
            sh.classList.remove('show');
        })
        _this.itens[0].classList.add('show');
        _this.btnPrev.removeAttribute('style');
        _this.btnNext.removeAttribute('style');

        addListeners();
    }

    function addListeners() {
        _this.btnPrev.addEventListener('click', showPrev);
        _this.btnNext.addEventListener('click', showNext);
    }

    function showPrev() {
        _currentQuote--;
        showQuote();
    }

    function showNext() {
        _currentQuote++;
        showQuote();
    }

    function showQuote() {
        var qtd = _this.itens.length;
        var Quote = _currentQuote % qtd;
        Quote = Math.abs(Quote);

        _this.container.querySelector('.show').classList.remove('show');
        _this.itens[Quote].classList.add('show');
    }
}