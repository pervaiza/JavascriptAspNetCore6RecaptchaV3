document.addEventListener("DOMContentLoaded", () => {
    if (document.getElementById("form-newuser")) {
        document.getElementById("form-newuser").addEventListener('submit', handleSubmit);
    }
});

function handleSubmit(e) {
    e.preventDefault();
    let thisForm = this;

    if (validateForm(thisForm)) {
        
        fetch("/Home/Index", {
            headers: {
                'Accept': 'application/json, text/plain',
                'Content-Type': 'application/json;charset=UTF-8'
            },
            method: "POST",
            body: JSON.stringify({
                FirstName: document.getElementById("FirstName").value,
                LastName: document.getElementById("LastName").value,
                Email: document.getElementById("Email").value,
                RecaptchaToken: document.getElementById("RecaptchaToken").value
            })
        }).then(res => res.json())
            .then(data => {
                if (data && data.success == true) {
                    thisForm.querySelector('.message').classList.add('d-block');
                    thisForm.querySelector('.message').innerHTML = "New user has been registered successfully";
                    thisForm.reset();
                }
                else {
                    console.log(thisForm, "Oops, there was an error. Please try again later.")
                    thisForm.querySelector('.message').classList.add('d-block');
                    thisForm.querySelector('.message').innerHTML = data.error;
                }
            })
            .catch(error => {
                console.log(thisForm, error)
            });
    }
}

function validateForm(form) {
    //Put your validation here 
    return true;
}