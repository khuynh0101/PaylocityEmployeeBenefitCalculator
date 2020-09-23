
const AddDependents = (event) => {
    event.preventDefault();

    const id = "a" + Math.floor(Math.random() * 90000) + 10000; 

    const dependentsContainer = document.querySelector("#dependentsContainer");
   
    const divContainerRow = document.createElement("div");
    divContainerRow.id = id;
    divContainerRow.classList.add("row");
    dependentsContainer.appendChild(divContainerRow);

    const divContainerCol = document.createElement("div");
    divContainerCol.id = id;
    divContainerCol.classList.add("col-md-7", "pb-1");
    divContainerRow.appendChild(divContainerCol);

    const input = document.createElement("input");
    input.classList = "form-control text-box single-line valid";
    input.type = "text";
    input.name = "dependent";
    divContainerCol.appendChild(input);

    const divContainerButtonCol = document.createElement("div");
    divContainerButtonCol.id = id;
    divContainerButtonCol.classList.add("col-md-5");
    divContainerRow.appendChild(divContainerButtonCol);

    const removeButton = document.createElement("button");
    removeButton.innerHTML = "Remove";
    removeButton.classList.add("btn", "btn-primary");
    removeButton.addEventListener("click", (event) => { RemoveDependents(event, id) });
    divContainerButtonCol.appendChild(removeButton);


    //divContainer.appendChild(input);
    //divContainer.appendChild(removeButton);
    //dependentsContainer.appendChild(divContainer);

    document.querySelector("#summary").remove();
}

const RemoveDependents = (event, id) => {
    event.preventDefault();
    document.querySelector("#" + id).remove();
    if (document.querySelector("#summary"))
        document.querySelector("#summary").remove();
}