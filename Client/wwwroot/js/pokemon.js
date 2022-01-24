

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
<h3 class="text-capitalize">${val.stat.name}</h3><span>${val.base_stat} %</span>
</div>

<div class="progress mb-3">
  <div class="progress-bar" role="progressbar" style="width: ${val.base_stat}%;""  aria-valuenow="${val.base_stat}" aria-valuemin="0" aria-valuemax="100"></div>
</div>`
            } else {
                stats += `
<div class="d-flex justify-content-between">
<h3 class="text-capitalize">${val.stat.name}</h3><span>${val.base_stat} %</span>
</div>
<div class="progress mb-3">
  <div class="progress-bar bg-danger" role="progressbar" style="width: ${val.base_stat}%;"  aria-valuenow="${val.base_stat}" aria-valuemin="0" aria-valuemax="100"></div>
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


