$(document).ready(function () {

    const imagePath = '../images/camisa1.png';

    const canvas = new fabric.Canvas('canvas');

    fabric.Image.fromURL('images/camisa1.png', function (img) {

        img.set({
            left: 70,  // Posição horizontal (x)
            top: 0,   // Posição vertical (y)
            scaleX: 1, // Escala horizontal (opcional)
            scaleY: 1  // Escala vertical (opcional)
        });
        
        canvas.setBackgroundImage(img);
        canvas.renderAll();
    });

    $('#addImageBtn').on('click', function () {
        const fileInput = document.getElementById('imageUpload');
        const file = fileInput.files[0];

        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                fabric.Image.fromURL(e.target.result, function (img) {
                    img.scaleToWidth(200); // Ajuste a largura conforme necessário
                    canvas.add(img);
                });
            };
            reader.readAsDataURL(file);
        }
    })

});