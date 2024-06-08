// Открытие модального окна
$('#popup-auth').click(function() {
    $('#popup-auth-wp').fadeIn(400);
    $('html').addClass('no-scroll');
});

// Закрытие модального окна при клике вне его области
$(document).mouseup(function(e) {
    var $popupAuthWp = $("#popup-auth-wp");
    var $popupWrapper = $(".popup-wrapper");
    
    // Если клик был не по модальному окну и не по его потомкам
    if (!$popupWrapper.is(e.target) && $popupWrapper.has(e.target).length === 0) {
        $popupAuthWp.fadeOut(200, function() {
            $('html').removeClass('no-scroll');
        });
    }
});

$('#popup-reg').click(function() {
    $('#popup-reg-wp').fadeIn(400);
    $('html').addClass('no-scroll');
});

// Закрытие модального окна при клике вне его области
$(document).mouseup(function(e) {
    var $popupRegWp = $("#popup-reg-wp");
    var $popupWrapper = $(".popup-wrapper");
    
    // Если клик был не по модальному окну и не по его потомкам
    if (!$popupWrapper.is(e.target) && $popupWrapper.has(e.target).length === 0) {
        $popupRegWp.fadeOut(200, function() {
            $('html').removeClass('no-scroll');
        });
    }
});
