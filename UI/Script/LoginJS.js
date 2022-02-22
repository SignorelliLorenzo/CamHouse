function e_validation(params) {
    var eform = document.getElementById("EForm");
    var e = document.getElementById("e-check").value;
    var emsg = document.getElementById("e-msg");

    var pattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;

    if(e.match(pattern)){
        eform.classList.add("valid");
        eform.classList.remove("invalid");
        emsg.innerHTML = "Your Email Address is Valid";
        emsg.style.color = "#00ff00";
    }else{
        eform.classList.remove("valid");
        eform.classList.add("invalid");
        emsg.innerHTML = "Please enter valid Email address!";
        emsg.style.color = "#ff0000";
    }

    if(e == ""){
        eform.classList.remove("valid");
        eform.classList.remove("invalid");
        emsg.innerHTML = " ";
        emsg.style.color = "#00ff00";
    }

}