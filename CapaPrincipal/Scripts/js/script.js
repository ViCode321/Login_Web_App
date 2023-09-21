/***Elimna los caracteres que no seán numéricos***/
$(document).ready(function () {
    $('#code').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
});

// Obtén una referencia al elemento de entrada numérica por su ID
var codeInput = document.getElementById("code");

/***Si el caracter es mayor de 4 dígitos, detiene la escritura***/
// Agrega un evento de escucha para el evento "input" en el campo
codeInput.addEventListener("input", function () {
    // Obtén el valor actual del campo de entrada
    var codeValue = this.value;

    // Verifica si la longitud del valor es mayor que 4
    if (codeValue.length > 4) {
        // Si la longitud es mayor que 4, corta el valor a 4 caracteres
        this.value = codeValue.slice(0, 4);
    }
});

