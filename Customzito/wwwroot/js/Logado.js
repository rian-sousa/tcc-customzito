$(document).ready(function () {

    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-tt="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    })
    

})


function RecuperarRoupasColecao(idColecao) {

    $.ajax({
        url: `ControleEstoque`, // Substitua "SuaAction" e "SeuController" pelos nomes reais
        type: 'GET', // ou 'GET', dependendo da sua action
        data: { idColecao },
        success: function (data) {
            // Lógica de manipulação de resultado, se necessário
            /*$('#PartialRoupas').html(data);*/
        },
        error: function (error) {
            // Lógica de tratamento de erro, se necessário
        }
    });
    
}

function AbrirModalEditarColecao(idColecao) {
    // Lógica para carregar os detalhes da coleção e preencher a partial view
    // Use AJAX para buscar os dados do servidor e preencher a partial view
    // Exemplo: $.get('/Colecao/Editar/' + idColecao, function(data) { $('#EditarColecaoPartial').html(data); });
    $.ajax({
        url: `EditarColecao/` + idColecao,
        type: 'GET',
        success: function (data) {
            $('#EditarColecaoPartial').html(data);
        }
    })
}

function MascaraValor(input) {

    $('#preco').inputmask('R$ 999,99 ', { placeholder: 'R$ ___,__' });
}