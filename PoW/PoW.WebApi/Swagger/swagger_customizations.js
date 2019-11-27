$(function() {

    // Renomeia a label 'Model' para 'Response'
    $(".response-class").find(".description-link").text("Response")

    $(".response-class").find(".snippet-link").text("Example")

    // Renomeia a label 'Model' para 'Request body'
    $(".operation-params").find(".description-link").text("Request body")

    $(".operation-params").find(".snippet-link").text("Example")

    // Remove a coluna 'Parameter Type'.
    $("table.parameters tr").find('td:eq(3),th:eq(3)').remove();

    // Diminui o tamanho da coluna 'Description'.
    $("table.parameters tr").find('th:eq(2)').attr("style", "width: 150px; max-width: 200px;");

    // Diminui o tamanho da coluna 'Data Type'.
    $("table.parameters tr").find('th:eq(3)').removeAttr("style");
});