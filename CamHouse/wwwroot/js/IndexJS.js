
//Function for remove page loader after click button "Join Now"
function RemovePageLoader(){
    document.getElementById("loader-body").style.display = "none";
    document.getElementById("BodyCustomIndex").style.backgroundColor = "#c0fce4"
}

function RegisterImgChange() {
    document.getElementById("imgindex").src = "/Img/indexregister.png";
}

function LoginImgChange() {
    document.getElementById("imgindex").src = "/Img/indexlogin.png";
}

function ResetImgChange() {
    document.getElementById("imgindex").src = "/Img/index.png";
}