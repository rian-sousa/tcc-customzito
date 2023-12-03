$(document).ready(function () {

    Dropzone.options.dzCustom = { // camelized version of the `id`
        paramName: "file", // The name that will be used to transfer the file
        maxFilesize: 50, // MB
        autoProcessQueue: true,
        accept: function (file, done) {
            if (file.name == "justinbieber.jpg") {
                done("Naha, you don't.");
            }
            else { done(); }
        }
    };

    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-tt="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });


})

function formatarValor(valor) {

    let numero = parseFloat(valor);



    // Verifica se o número possui parte decimal
    if (numero % 1 !== 0) {
        const valorFormatado = numero.toLocaleString('pt-BR', {
            style: 'decimal',
            currency: 'BRL',
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
            useGrouping: false
        });
        return valorFormatado;
    } else {
        // Se não houver parte decimal, adiciona ".00" manualmente
        const valorFormatado = numero.toLocaleString('pt-BR', {
            style: 'decimal',
            currency: 'BRL',
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
            useGrouping: false
        });
        return valorFormatado;
    }
}

function formatarValores() {
    $(".produtos-text-preco").each(function () {
        // Obtenha o valor atual do elemento
        let valorAtual = $(this).text()

        let valorFormatado = valorAtual.replace(/^(\d{3})(\d{2})$/, 'R$ $1,$2');
       
        $(this).text(valorFormatado);
    });
}

