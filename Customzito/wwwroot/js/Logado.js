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