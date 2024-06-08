const swiperDisease = new Swiper('.disease-swiper--container', {
    // Optional parameters
    direction: 'horizontal',
    
    // Navigation arrows
    navigation: {
        nextEl: '.swiper-button-next',
        prevEl: '.swiper-button-prev',
        
    },

    pagination:{
        el: '.swiper-pagination',
        clickable: true,
    },

    loop: false,
    slidesPerView: 2,
    spaceBetween: 20,
});


// const swiper = new Swiper('.swiper-container', {
//     navigation: {
//         nextEl: '.swiper-button-next',
//         prevEl: '.swiper-button-prev',
//     },
// });