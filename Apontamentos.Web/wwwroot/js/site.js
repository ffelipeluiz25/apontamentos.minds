$(document).ready(function () {
    RecuperaMesAtual();
    RecuperaAnoAtual();
    EventosTelaIndex();
    Filtrar();
});

function RecuperaMesAtual() {
    var mes = (new Date()).getMonth();
    mes++;
    if (mes < 10) {
        mes = "0" + mes;
    }
    else
        mes = mes;

    $('#selMes').val(mes);
    return mes;
}

function RecuperaAnoAtual() {
    var ano = (new Date()).getFullYear();
    $('#selAno').val(ano);
    return ano;
}

function Filtrar() {
    var data = RecuperaDataFiltro();
    var usuario = $('#selResponsavel').val();
    $.ajax({
        type: "POST",
        url: "/Home/Filtros",
        data: { data: data, usuario: usuario },
        success: function (response) {
            $("#divTabela").html(response);
            EventosTelaPartialApontamentos();
        },
        error: function (request, textStatus, errorThrown) {
        }
    });
}

function EventosTelaIndex() {
    $('#selMes').on('change', function (e) {
        Filtrar();
    });

    $('#selAno').on('change', function (e) {
        Filtrar();
    });

    $('#selResponsavel').on('change', function (e) {
        Filtrar();
    });
}

function EventosTelaPartialApontamentos() {
    $("#ApontamentosGrid .details").click(function (e) {
        var dataSelecionada = $(this).parent().parent().find('[id*=dtDataFormatada]').text();
        var dataBuscaPorPeriodo = RecuperaDataFiltro();
        var usuario = $('#selResponsavel').val();
        $.ajax({
            type: "POST",
            url: "/Home/Details",
            data: { dataSelecionada: dataSelecionada, dataBuscaPorPeriodo: dataBuscaPorPeriodo, usuario: usuario },
            success: function (response) {
                $("#detalhesModal").find(".modal-body").html(response);
                $("#detalhesModal").modal('show');
            },
            error: function (request, textStatus, errorThrown) {
            }
        });
    });

    $("#detalhesModal .fechar").click(function () {
        $("#detalhesModal").modal('hide');
    });

}

function RecuperaDataFiltro() {
    var dia = '01';
    var mes = $('#selMes').val();
    var ano = $('#selAno').val();

    var data = ano + "-" + mes + "-" + dia;
    return data;
}