
const AddDependents = (event) => {
    event.preventDefault();

    const id = "a" + Math.floor(Math.random() * 90000) + 10000; 

    const dependentsContainer = document.querySelector("#dependentsContainer");
   
    const input = document.createElement("input");
    input.classList = "form-control text-box single-line valid";
    input.type = "text";
    input.name = "dependent";
    dependentsContainer.appendChild(input);

    const divContainer = document.createElement("div");
    divContainer.id = id;
    divContainer.classList.add("form-inline");

    const removeButton = document.createElement("button");
    removeButton.innerHTML = "Remove";
    removeButton.addEventListener("click", (event) => { RemoveDependents(event, id) });

    divContainer.appendChild(input);
    divContainer.appendChild(removeButton);
    dependentsContainer.appendChild(divContainer);

    document.querySelector("#summary").remove();
}

const RemoveDependents = (event, id) => {
    event.preventDefault();
    document.querySelector("#" + id).remove();
    document.querySelector("#summary").remove();
}