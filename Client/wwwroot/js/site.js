// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$.ajax({
    url: 'https://pokeapi.co/api/v2/pokemon'
}).done((data) => {
    var textPokemon = '';
    $.each(data.results, function (key, val) {
        key++;
        textPokemon += `<tr>
                    <td>${key}</td>
                    <td class="text-capitalize">${val.name}</td>
                    <td><button class="btn btn-primary btn-sm" onclick="getDetail('${val.url}')" data-toggle="modal" data-target="#pokeModal">Detail</button></td>
                </tr>`
    });
    $(".pokeTable").html(textPokemon);

}).fail((error) => {
    console.log(error);
})

function getDetail(url) {
    $.ajax({
        url: url
    }).done((data) => {
        var textDetailPokemon = '';
        textDetailPokemon += `<h5 class="modal-title text-capitalize" id="exampleModalLabel">${data.name}</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>`
        $(".modal-header").html(textDetailPokemon);

        var imagePokemon = '';
        imagePokemon += `<img src="${data.sprites.other.home.front_default}" class="img-thumbnail" alt="front_default">`
        $(".photo").html(imagePokemon);

        var type = '';
        $.each(data.types, function (key, val) {

            if (val.type.name == "poison") {
                type += `<span class="badge poison p-1 mr-3">${val.type.name}</span>`
            } else if (val.type.name == "grass") {
                type += `<span class="badge grass p-1 mr-3">${val.type.name}</span>`
            }

        });
        $(".type").html(type);


        var height = '';
        height += `${data.height} "`;
        $(".height").html(height);

        var weight = '';
        weight += `${data.weight} lbs`;
        $(".weight").html(weight);

        var abilities = '';
        $.each(data.abilities, function (key, val) {

            if (data.abilities.length - 1 == key) {
                abilities += `${val.ability.name}.`
            } else {
                abilities += `${val.ability.name}, `
            }

        });

        $(".abilities").html(abilities);

        var stats = '';
        $.each(data.stats, function (key, val) {

            if (val.base_stat > 50) {
                stats += `
<div class="d-flex justify-content-between">
<h3 class="text-capitalize">${ val.stat.name}</h3><span>${val.base_stat} %</span>
</div>

<div class="progress mb-3">
  <div class="progress-bar" role="progressbar" style="width: ${val.base_stat}%;""  aria-valuenow="${ val.base_stat}" aria-valuemin="0" aria-valuemax="100"></div>
</div>`
            } else {
                stats += `
<div class="d-flex justify-content-between">
<h3 class="text-capitalize">${ val.stat.name}</h3><span>${val.base_stat} %</span>
</div>
<div class="progress mb-3">
  <div class="progress-bar bg-danger" role="progressbar" style="width: ${val.base_stat}%;"  aria-valuenow="${ val.base_stat}" aria-valuemin="0" aria-valuemax="100"></div>
</div>`
            }

        });

        $(".stats").html(stats);

        var imageSpritesFront = '';
        imageSpritesFront += `<img src="${data.sprites.front_default}" class="img-thumbnail" alt="front_default"><p>Front Default</p>`
        $(".sprites-front").html(imageSpritesFront);

        var imageSpritesBack = '';
        imageSpritesBack += `<img src="${data.sprites.back_default}" class="img-thumbnail" alt="back_default"><p>Back Default</p>`
        $(".sprites-back").html(imageSpritesBack);

    });

}

function htmlFunction() {


    document.getElementsByClassName('title')[0].innerHTML = 'The Basic Languange of the web : HTML';

    document.getElementsByClassName('date-post')[0].innerHTML = 'Posted by <b>Mozilla</b> on Monday, June 21st 2027';
    document.getElementById('image').src = '/image/pic_html.jpeg';

    document.getElementsByClassName('body-topic')[0].innerHTML = 'HTML (HyperText Markup Language) is the most basic building block of the Web. It defines the meaning and structure of web content. Other technologies besides HTML are generally used to describe a web page appearance/presentation (CSS) or functionality / behavior(JavaScript).';
}

function cssFunction() {

    document.getElementsByClassName('title')[0].innerHTML = 'The Basic Languange of the web : CSS';

    document.getElementsByClassName('date-post')[0].innerHTML = 'Posted by <b>Mozilla</b> on Saturday, June 24st 2027';
    document.getElementById('image').src = '/image/pic_css.png';

    document.getElementsByClassName('body-topic')[0].innerHTML = 'Cascading Style Sheets (CSS) is a stylesheet language used to describe the presentation of a document written in HTML or XML (including XML dialects such as SVG, MathML or XHTML). CSS describes how elements should be rendered on screen, on paper, in speech, or on other media.';
}


function javascriptFunction() {


    document.getElementsByClassName('title')[0].innerHTML = 'The Basic Languange of the web : JavaScript';

    document.getElementsByClassName('date-post')[0].innerHTML = 'Posted by <b>Mozilla</b> on Sunday, June 28st 2027';
    document.getElementById('image').src = '/image/pic_javascript.jpg';

    document.querySelector('.body-topic').innerHTML = 'JavaScript (JS) is a lightweight, interpreted, or just-in-time compiled programming language with first-class functions. While it is most well-known as the scripting language for Web pages, many non-browser environments also use it. ';

}

function animalFunction() {

    /*data animals*/
    let animals = [
        { name: 'bimo', species: 'cat', kelas: { name: "mamalia" } },
        { name: 'budi', species: 'cat', kelas: { name: "mamalia" } },
        { name: 'nemo', species: 'snail', kelas: { name: "invertebrata" } },
        { name: 'dori', species: 'cat', kelas: { name: "mamalia" } },
        { name: 'simba', species: 'snail', kelas: { name: "invertebrata" } }
    ]


    /*new array use store data only cat*/
    let onlyCat = [];

    /*add data species cat in onlyCat*/
    for (var i = 0; i < animals.length; i++) {
        if (animals[i].species == "cat") {
            onlyCat.push(animals[i]);
        }
    }
    console.log(onlyCat);

    /*replace class name non-mamalia if species snail */
    for (var i = 0; i < animals.length; i++) {
        if (animals[i].species == "snail") {
            animals[i].kelas.name = "Non-Mamalia"
        }
    }
    console.log(animals);


    let arr = ['Apple', { name: 'John' }, true, function () { alert('Hello'); }];
    console.log(arr[1].name);
}