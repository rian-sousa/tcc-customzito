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

})

function formatarValor(valor) {
    let numero = parseFloat(valor);

    const valorFormatado = numero.toLocaleString('pt-BR', {
        style: 'currency',
        currency: 'BRL',
        minimumFractionDigits: 2,
        maximumFractionDigits: 0,
    });

    return valorFormatado;
}

function formatarValores() {
    $('label.produtos-text-preco').each(function () {
        // Obtenha o valor atual do elemento
        var valorAtual = $(this).text().replace('R$', '');

        // Formate o valor usando a função formatarMoeda
        var valorFormatado = formatarValor(valorAtual);

        // Substitua o conteúdo do elemento pelo valor formatado
        $(this).text(valorFormatado);
    });
}