(function ($) {
    if ($('#accommodations').length) {
        $("#accommodations .accommodation-type").each(function (i) {

            var _this = $(this);
            var _allVillaBackground = $('.accommodation-bg-change');
            var _villaBackground = _this.find('.accommodation-bg-change');

            $(window).resize(function () {
                if ($(window).width() < 900) {
                    _villaBackground.css({
                        'background-image': "url(" + _villaBackground.attr('data-src') + ")"
                    });

                }
            });
            _this.hover(function () {
                if ($(window).width() > 900) {
                    _allVillaBackground.css({ 'background-image': "url(" + _villaBackground.attr('data-src') + ")" });
                } else {
                    _villaBackground.css({ 'background-image': "url(" + _villaBackground.attr('data-src') + ")" });
                }
                return false;
            });
        });
    }
    let apiKey = '5acec52fcaaa1a1a719079e8b070249c',
        corLat = '-0.13',
        corLong = '51.51';

    

}(jQuery));