(function ($) {
    $.fn.changeText = function () {
        return this.each(function () {
            $(this).css('font-size', '50px');
        });
    }

    $.fn.load = function (action = 'load') {
        return this.each(function () {
            switch (action) {
                case 'load':
                    let divToAppend = document.createElement('div');

                    divToAppend.id = 'loader';
                    divToAppend.className = 'loading';

                    let divLoad = () => {
                        let divElement = document.createElement('div');
                        divElement.className = 'lds-ripple';

                        let divChild1 = document.createElement('div');
                        let divChild2 = document.createElement('div');

                        $(divElement).append(divChild1);
                        $(divElement).append(divChild2);

                        return divElement;
                    };

                    $(divToAppend).append(divLoad);
                    $(this).append(divToAppend).hide().fadeIn('slow');

                    break;
                case 'unload':
                    $('#loader').fadeOut(function () {
                        $(this).remove();
                    })
                    break;
                default:
                    console.error('Informe uma ação valida');
            }
        });
    }
})(jQuery);

$(() => {

});