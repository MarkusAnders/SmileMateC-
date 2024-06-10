const dropZone = document.getElementById('dropZone');
const fileInput = document.getElementById('fileInput');
const clearFilesButton = document.getElementById('clearFilesButton');
const previewContainer = document.getElementById('previewContainer');

["dragover", "drop"].forEach(event => {
    document.addEventListener(event, evt => {
        evt.preventDefault();
    });
});

dropZone.addEventListener("dragenter", () => {
    dropZone.classList.add("_active");
});

dropZone.addEventListener("dragleave", () => {
    dropZone.classList.remove("_active");
});

dropZone.addEventListener("drop", event => {
    dropZone.classList.remove("_active");
    const files = event.dataTransfer.files;
    handleFiles(files);
});

fileInput.addEventListener("change", event => {
    const files = event.target.files;
    handleFiles(files);
});

clearFilesButton.addEventListener("click", () => {
    fileInput.value = "";
    previewContainer.innerHTML = "";
});

function handleFiles(files) {
    previewContainer.innerHTML = "";
    const dataTransfer = new DataTransfer();

    Array.from(files).forEach(file => {
        if (file.type.startsWith("image/")) {
            const img = document.createElement("img");
            img.src = URL.createObjectURL(file);
            previewContainer.appendChild(img);
            dataTransfer.items.add(file);
        }
    });

    fileInput.files = dataTransfer.files;
}



//const dropZone = document.getElementById('dropZone');
//const fileInput = document.getElementById('fileInput');
//const clearFilesButton = document.getElementById('clearFilesButton');
//const previewContainer = document.getElementById('previewContainer');

//["dragover", "drop"].forEach(event => {
//    document.addEventListener(event, evt => {
//        evt.preventDefault();
//    });
//});

//dropZone.addEventListener("dragenter", () => {
//    dropZone.classList.add("_active");
//});

//dropZone.addEventListener("dragleave", () => {
//    dropZone.classList.remove("_active");
//});

//dropZone.addEventListener("drop", event => {
//    dropZone.classList.remove("_active");
//    const files = event.dataTransfer.files;
//    handleFiles(files);
//});

//fileInput.addEventListener("change", event => {
//    const files = event.target.files;
//    handleFiles(files);
//});

//clearFilesButton.addEventListener("click", () => {
//    fileInput.value = "";
//    previewContainer.innerHTML = "";
//});

//function handleFiles(files) {
//    previewContainer.innerHTML = "";
//    Array.from(files).forEach(file => {
//        if (file.type.startsWith("image/")) {
//            const img = document.createElement("img");
//            img.src = URL.createObjectURL(file);
//            previewContainer.appendChild(img);
//        }
//    });
//}

function text(x){
    if(x==0)document.getElementById("inputBlock").style.display = "block";
    else document.getElementById("inputBlock").style.display = "none";
    return;
}