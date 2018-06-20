function buscarCidades() {
    var id_estado = document.getElementById("id_estado").value;
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/Cidade/ListaCidadeEstado/" + id_estado);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                var select = document.getElementById("id_cidade");
                //console.log(xhr.responseText);
                var cidades = JSON.parse(xhr.responseText);
                select.innerText = null;
                select.innerHTML = "<option></option>"

                for (var i = 0; i < cidades.length; i++) {
                    select.innerHTML += "<option value=" + cidades[i].Id + ">" + cidades[i].Nome + "</option>"
                }
            }
        }
    }
    xhr.send();
}

function buscarModelos() {
    var id_marca = document.getElementById("id_marca").value;
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/Veiculo/ListaModelosMarca/" + id_marca);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                var select = document.getElementById("id_modelo");
                //console.log(xhr.responseText);
                var modelos = JSON.parse(xhr.responseText);
                select.innerText = null;
                select.innerHTML = "<option></option>"

                for (var i = 0; i < modelos.length; i++) {
                    select.innerHTML += "<option value =" + modelos[i].Id + ">" + modelos[i].Descricao + "</option>"
                }
            }
        }
    }
    xhr.send();
}

function buscarVeiculos() {
    var id_cliente = document.getElementById("id_cliente").value;
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/Cliente/ListaVeiculos/" + id_cliente);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                var select = document.getElementById("id_veiculo");
                //console.log(xhr.responseText);
                var veiculos = JSON.parse(xhr.responseText);
                select.innerText = null;
                select.innerHTML = "<option></option>"

                for (var i = 0; i < veiculos.length; i++) {
                    select.innerHTML += "<option value =" + veiculos[i].Id + ">" + veiculos[i].Placa + "</option>"
                }
            }
        }
    }
    xhr.send();
}

function buscarUltimoNumeroOS() {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/AberturaOS/UltimoNumeroOS/");
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                console.log(xhr.responseText);
                itens = JSON.parse(xhr.responseText);
                document.getElementById("Numero").value = itens;
                document.getElementById("Numero").readOnly = true;

            }
        }
    }
    xhr.send();
}


function preencheTotal() {
        document.getElementById("valor_total").value = parseFloat(document.getElementById("Quantidade").value) * parseFloat(document.getElementById("ValorUnitario").value);
}
let produtos;
function buscarProdutos() {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/ExecucaoOS/ListaProdutos/");
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                var select = document.getElementById("ITENS");
                console.log(xhr.responseText);
                produtos = JSON.parse(xhr.responseText);
                select.innerHTML = null;
                select.innerHTML = "<option></option>"
                for (var i = 0; i < produtos.length; i++) {
                    select.innerHTML += "<option value = " + produtos[i].Id + "> " + produtos[i].Descricao + "</option>"
                }
            }
        }
    }
    xhr.send();
};

function buscaValor(index, num) {
    document.getElementById('valorItem' + num).value = produtos[index - 1].ValorUnitario;
    calcula();
}

var nItem = 1;
var ITENS = 0;

function deleteRow(row) {
    var i = row.parentNode.parentNode.rowIndex;
    document.getElementById('TabelaProdutoItem').deleteRow(i);
    
}

function calcula(num) {
    document.getElementById('somaItem' + num).value = document.getElementById('valorItem' + num).value * document.getElementById('qtdItem' + num).value;
}

function insRow() {
    var x = document.getElementById('TabelaProdutoItem').insertRow(1);

    x.innerHTML = " <tr>" +
        "   <td align='center'>" + nItem + "</td>" +
        " <td><select onchange='buscaValor(this.selectedIndex, " + nItem + ")' id='ITENS' name='ITENS'></select></td> " +
        " <td><input onkeyup='calcula(" + nItem + ")' id='valorItem" + nItem + "' value=' '  name='valorItem' readonly='readonly' style='text-align: center' /></td> " +
        " <td><input onkeyup='calcula(" + nItem + ")' id='qtdItem" + nItem + "' value=' ' name='qtdItem' style='text-align: center' /></td> " +
        " <td><input size=15 id='somaItem" + nItem + "' readonly='readonly' style='text-align: center' /></td > " +
        " <td><input type='button' id='delButton' value='Excluir Item' onclick='deleteRow(this)' /></td>  " +
        "    </tr>";
    nItem++;
    buscarProdutos();

}

function onSubmit() {
    var dados = $("form").serializeArray();
    console.log(dados);
    $.ajax({
        url: '/ExecucaoOS/InserirItens',
        type: 'POST',
        dataType: 'json',
        data: dados,
        success: function (data, textStatus, xhr) {
            console.log(data);
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log(textStatus);
        }
    });

    return false; //don't submit

}

function CancelarOS() {
    var dados = document.getElementById("id_os").value;
    if (dados != "") {
        console.log(dados);
        $.ajax({
            url: '/FechamentoOS/Cancelar/' + dados,
            type: 'POST',
            dataType: 'text',
            data: dados,
            success: function (data, textStatus, xhr) {
                console.log(data);
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log(textStatus);
            }
        });
    }
    

    return false; //don't submit

}


function buscarItensOS() {
    
    var id_os = document.getElementById("id_os").value;
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/FechamentoOS/ListaItens/" + id_os);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                console.log(xhr.responseText);
                itens = JSON.parse(xhr.responseText);


                var tabela = document.getElementById("tablebody");//.insertRow(1);
                tabela.innerHTML = "";
                for (var i = 0; i < itens.length; i++) {
                    tabela.innerHTML += "<tr>" +
                        "<td>" + (i + 1) + "</td>" +
                        "<td>" + itens[i].Produto.Descricao + "</td>" +
                        "<td>" + itens[i].Produto.ValorUnitario + "</td>" +
                        "<td>" + itens[i].Quantidade + "</td>" +
                        "<td>" + (itens[i].Produto.ValorUnitario * itens[i].Quantidade) + "</td>" +
                        "</tr>";
                }
            }
        }
    }
    xhr.send();
}

function buscarValorOS() {

    var id_os = document.getElementById("id_os").value;
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/FechamentoOS/ValorTotalOS/" + id_os);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                //console.log(xhr.responseText);
                itens = JSON.parse(xhr.responseText);
                document.getElementById("ValorTotal").value = itens;
                document.getElementById("ValorTotal").readOnly = true;

                }
            }
        }
    xhr.send();
}


function validaCPF(s) {
    var i;
    var l = '';
    for (i = 0; i < s.length; i++) if (!isNaN(s.charAt(i))) l += s.charAt(i);
    s = l;
    if (s.length != 11) return false;
    var c = s.substr(0, 9);
    var dv = s.substr(9, 2);
    var d1 = 0;
    for (i = 0; i < 9; i++) d1 += c.charAt(i) * (10 - i);
    if (d1 == 0) return false;
    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;
    if (dv.charAt(0) != d1) return false;
    d1 *= 2;
    for (i = 0; i < 9; i++) d1 += c.charAt(i) * (11 - i)
    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;
    if (dv.charAt(1) != d1) return false;
    return true;
}

$.validator.addMethod("customvalidationcpf", function (value, element, param) {
    return validaCPF(value); //chama um método validaCPF implementado em javascript
});
$.validator.unobtrusive.adapters.addBool("customvalidationcpf");