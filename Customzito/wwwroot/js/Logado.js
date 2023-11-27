// Valores constantes
const meses = [
    "Janeiro", "Fevereiro", "Março",
    "Abril", "Maio", "Junho",
    "Julho", "Agosto", "Setembro",
    "Outubro", "Novembro", "Dezembro"];


$(document).ready(function () {

    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-tt="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });


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

function MascaraValor(input) {
    $('#preco').inputmask('R$ 999,99 ', { placeholder: 'R$ ___,__' });
}

function RemoverColecao(idColecao) {

    $.ajax({
        url: `RemoverColecao/` + idColecao,
        type: 'POST',
        success: function (data) {
            $('#RemoverColecaoModalBody').empty()
            $('#RemoverColecaoModalBody').html(data);
        }
    })
}
function AdicionarColecao() {
    let descricao = $('#DescricaoColecao').val();
    let limitado = $('#checkLimitado');

    let limitadoValue = limitado.is(':checked') ? 'true' : 'false';

    $.ajax({
        url: `AdicionarColecao/` + '?DescricaoColecao=' + descricao + '&checkLimitado=' + limitadoValue,
        type: 'POST',
        success: function (data) {
            $('#AdicionarColecaoModalBody').empty()
            $('#AdicionarColecaoModalBody').html(data);
        }
    })
}

function EditarColecao() {
    let InputId = $('#InputIdColecao').val();
    let Ndescricao = $('#NovaDescricao').val();
    let limitado = $('#checkLimitado');

    let limitadoValue = limitado.is(':checked') ? 'true' : 'false';

    $.ajax({
        url: 'EditarColecao/' + '?id=' + InputId + "&descricao=" + Ndescricao + '&limitado=' + limitadoValue,
        type: 'POST',
        success: function (data) {
            $('#EditarColecaoPartial').empty()
            $('#EditarColecaoPartial').html(data);
        }
    })
}

function AbrirModalEditarColecao(idColecao) {

    $.ajax({
        url: `RestaurarModalEditarColecao/` + idColecao,
        type: 'GET',
        success: function (data) {
            $('#EditarColecaoPartial').html(data);
        }
    })
}

function RestaurarModalColecaoAdd(modal) {
    $.ajax({
        url: modal,
        type: 'GET',
        success: function (data) {
            $('#AdicionarColecaoModalBody').empty()
            $('#AdicionarColecaoModalBody').html(data);
        }
    })
}

function AbrirModalColecaoRemover(modal,id) {
    $.ajax({
        url: modal + `?idColecao=${id}`,
        type: 'GET',
        success: function (data) {
            $('#RemoverColecaoModalBody').empty()
            $('#RemoverColecaoModalBody').html(data);
        }
    })
}

function AbrirModalRoupaAdd(modal) {
    $.ajax({
        url: modal,
        type: 'GET',
        success: function (data) {
            $('#AdicionarColecaoModalBody').empty()
            $('#AdicionarColecaoModalBody').html(data);
        }
    })
}

function AbrirModalEditarRoupa(idRoupa) {

    $.ajax({
        url: `RestaurarModalEditarRoupa/` + idRoupa,
        type: 'GET',
        success: function (data) {
            $('#EditarRoupaPartial').html(data);
        }
    })
}

function RestaurarModalRoupaAdd(modal) {
    $.ajax({
        url: modal,
        type: 'GET',
        success: function (data) {
            $('#AdicionarRoupaModalBody').empty()
            $('#AdicionarRoupaModalBody').html(data);
        }
    })
}

function RestaurarModalRoupaRemover(modal) {
    $.ajax({
        url: modal,
        type: 'GET',
        success: function (data) {
            $('#AdicionarRoupaModalBody').empty()
            $('#AdicionarRoupaModalBody').html(data);
        }
    })
}

function AbrirModalRoupaRemover(modal, id) {
    $.ajax({
        url: modal + `?idRoupa=${id}`,
        type: 'GET',
        success: function (data) {
            $('#RemoverRoupaModalBody').empty()
            $('#RemoverRoupaModalBody').html(data);
        }
    })
}

function RemoverRoupa(idRoupa) {
    $.ajax({
        url: `RemoverRoupa/` + idRoupa,
        type: 'POST',
        success: function (data) {
            $('#RemoverRoupaModalBody').empty()
            $('#RemoverRoupaModalBody').html(data);
        }
    })
}


function AdicionarRoupa() {
    let DescricaoRoupa = $('#DescricaoRoupa').val();
    let Titulo = $('#Titulo').val();
    let preco = $('#preco').val();
    let IdTipoVestimenta = $('#IdTipoVestimenta').val();
    let qtd = $('#qtd').val();
    let IdMaterial = $('#IdMaterial').val();
    let IdColecao = $('#IdColecao').val();
    let Marca = $('#Marca').val();
    let imagem = $('#imagem').val();
    

    $.ajax({
        url: `AdicionarRoupa/` + '?DescricaoRoupa=' + DescricaoRoupa + '&Titulo=' + Titulo
            + '&preco=' + preco + '&IdTipoVestimenta=' + IdTipoVestimenta + '&qtd=' + qtd
            + '&IdMaterial=' + IdMaterial + '&IdColecao=' + IdColecao + '&Marca=' + Marca
            + '&imagem=' + imagem,
        type: 'POST',
        success: function (data) {
            $('#AdicionarRoupaModalBody').empty()
            $('#AdicionarRoupaModalBody').html(data);
        }
    })
}

function EditarRoupa() {
    let DescricaoRoupa = $('#DescricaoRoupa').val();
    let Titulo = $('#Titulo').val();
    let preco = $('#preco').val();
    let IdTipoVestimenta = $('#IdTipoVestimenta').val();
    let qtd = $('#qtd').val();
    let IdMaterial = $('#IdMaterial').val();
    let IdColecao = $('#IdColecao').val();
    let Marca = $('#Marca').val();
    let imagem = $('#imagem').val();
    let idProduto = $('#idProduto').val();

    $.ajax({
        url: `EditarRoupa/` + '?idProduto' + idProduto + '&DescricaoRoupa=' + DescricaoRoupa + '&Titulo=' + Titulo
            + '&preco=' + preco + '&IdTipoVestimenta=' + IdTipoVestimenta + '&qtd=' + qtd
            + '&IdMaterial=' + IdMaterial + '&IdColecao=' + IdColecao + '&Marca=' + Marca
            + '&imagem=' + imagem ,
        type: 'POST',
        success: function (data) {
            $('#EditarRoupaPartial').empty()
            $('#EditarRoupaPartial').html(data);
        }
    })
}


function gerarGraficoFaturamentoMensal(decimais) {
    new Chart(faturamentoMensal, {
        type: 'line',
        data: {
            labels: meses,
            datasets: [{
                label: "Vendas Totais",
                data: decimais,
                borderColor: '#800080',
                fill: false,
            }
            //{
            //    label: "Boletos",
            //    data: listaBoletosMensal,
            //    borderColor: '#4bc0c0',
            //    fill: false,
            //}
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                tooltip: {
                    callbacks: {
                        label: ctx => new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL', maximumSignificantDigits: 3 }).format(ctx.parsed.y)
                    }
                },
                title: {
                    display: false,
                    text: "Faturamento Mensal",
                    padding: {
                        top: 10,
                        bottom: 5
                    },
                    font: {
                        size: 20,
                    }
                }
            },
            scales: {
                y: {
                    ticks: {
                        callback: function (value, index, values) {
                            return new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL', maximumSignificantDigits: 3 }).format(value);
                        }
                    }
                }
            },
            stacked: true,
            responsive: true,
        }
    });
}


function gerarGraficoFaturamentoMensalCustomFabricados(decimalCustom, decimalFabricado) {
    new Chart(faturamentoMensalTipo, {
        type: 'line',
        data: {
            labels: meses,
            datasets: [{
                label: "Vendas - Customizados",
                data: decimalCustom,
                borderColor: '#C6262C',
                fill: false,
            },
            {
                label: "Vendas - Fabricados",
                data: decimalFabricado,
                borderColor: '#4bc0c0',
                fill: false,
            }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                tooltip: {
                    callbacks: {
                        label: ctx => new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL', maximumSignificantDigits: 3 }).format(ctx.parsed.y)
                    }
                },
                title: {
                    display: false,
                    text: "Faturamento Mensal",
                    padding: {
                        top: 10,
                        bottom: 5
                    },
                    font: {
                        size: 20,
                    }
                }
            },
            scales: {
                y: {
                    ticks: {
                        callback: function (value, index, values) {
                            return new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL', maximumSignificantDigits: 3 }).format(value);
                        }
                    }
                }
            },
            stacked: true,
            responsive: true,
        }
    });
}


function gerarGraficoVendasTipos(collections, products) {

    const nomesColecoes = collections.map(item => item[0]);
    const vendasColecoes = collections.map(item => item[1]);

    const nomesProdutos = products.map(item => item[0]);
    const vendasProdutos = products.map(item => item[1]);

    new Chart(VendasColecao, {
        type: 'bar',
        data: {
            labels: nomesColecoes,
            datasets: [{
                label: 'Vendas por Coleção',
                data: vendasColecoes,
                backgroundColor: '#C6262C',
                borderColor: '#C6262C',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                tooltip: {
                    callbacks: {
                        label: (tooltipItem) => {
                            return new Intl.NumberFormat('pt-BR', {
                                style: 'currency',
                                currency: 'BRL',
                                maximumSignificantDigits: 3
                            }).format(tooltipItem.parsed.y);
                        }
                    }
                },
                title: {
                    display: true,
                    text: 'Vendas por Coleção',
                    font: {
                        size: 20
                    }
                }
            },
            scales: {
                y: {
                    ticks: {
                        callback: (value) => {
                            return new Intl.NumberFormat('pt-BR', {
                                style: 'currency',
                                currency: 'BRL',
                                maximumSignificantDigits: 3
                            }).format(value);
                        }
                    }
                }
            }
        }
    });

    new Chart(VendasProduto, {
        type: 'bar',
        data: {
            labels: nomesProdutos,
            datasets: [{
                label: 'Vendas por Produto',
                data: vendasProdutos,
                backgroundColor: '#4bc0c0',
                borderColor: '#4bc0c0',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                tooltip: {
                    callbacks: {
                        label: (tooltipItem) => {
                            return new Intl.NumberFormat('pt-BR', {
                                style: 'currency',
                                currency: 'BRL',
                                maximumSignificantDigits: 3
                            }).format(tooltipItem.parsed.y);
                        }
                    }
                },
                title: {
                    display: true,
                    text: 'Vendas por Produto',
                    font: {
                        size: 20
                    }
                }
            },
            scales: {
                y: {
                    ticks: {
                        callback: (value) => {
                            return new Intl.NumberFormat('pt-BR', {
                                style: 'currency',
                                currency: 'BRL',
                                maximumSignificantDigits: 3
                            }).format(value);
                        }
                    }
                }
            }
        }
    });
}