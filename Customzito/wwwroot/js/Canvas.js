
function MostrarCanvasCustom() {

    
}

function SalvarCustom() {
    let a = document.createElement('a')
    let dt = this.canvas.toDataURL({
        format: 'png',
        quality: 1,
    })
    dt = dt.replace(/^data:image\/[^;]*/, 'data:application/octet-stream')
    dt = dt.replace(
        /^data:application\/octet-stream/,
        'data:application/octet-stream;headers=Content-Disposition%3A%20attachment%3B%20filename=Canvas.png',
    )

    a.href = dt
    a.download = 'canvas.png'
    a.click()
}



$(document).ready(function () {

    const imagePath = '../images/camisa1.png';

    const canvas = new fabric.Canvas('canvas');

    fabric.Image.fromURL('images/custom-bases/cortavento.png', function (img) {

        let canvasWidth = canvas.getWidth();
        let canvasHeight = canvas.getHeight();

        // Calcula as coordenadas para centralizar a imagem
        let left = (canvasWidth - img.width * img.scaleX) / 2;
        let top = (canvasHeight - img.height * img.scaleY) / 2;

        img.set({
            left: left,
            top: top,
            scaleX: 1, 
            scaleY: 1, 
            selectable: false // Evita que a imagem seja selecionável
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
    });

    $('.opt').on('click', function () {
        var imageUrl = $(this).data('src');

        fabric.Image.fromURL(imageUrl, function (img) {

            let canvasWidth = canvas.getWidth();
            let canvasHeight = canvas.getHeight();

            // Calcula as coordenadas para centralizar a imagem
            let left = (canvasWidth - img.width * img.scaleX) / 2;
            let top = (canvasHeight - img.height * img.scaleY) / 2;

            img.set({
                left: left,
                top: top,
                scaleX: 1, 
                scaleY: 1, 
                selectable: false 
            });

            canvas.setBackgroundImage(img, canvas.renderAll.bind(canvas));
        });
    });

    $('#salvar').on('click', function () {
        SalvarCustom();        
    })

});