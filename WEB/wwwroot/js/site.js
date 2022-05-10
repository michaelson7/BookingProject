var baseUrl = 'http://localhost:3060/';

//highlights selected page in menu
(function ($) {
    "use strict";

    // Add active state to sidbar nav links
    var path = window.location.href; // because the 'href' property of the DOM element is the absolute path
    $("#layoutSidenav_nav .sb-sidenav a.nav-link").each(function () {
        if (this.href === path) {
            $(this).addClass("active");
        }
    });

    // Toggle the side navigation
    $("#sidebarToggle").on("click", function (e) {
        e.preventDefault();
        $("body").toggleClass("sb-sidenav-toggled");
    });
})(jQuery);

$(document).ready(function () {
    try {

        //progress inidcator
        NProgress.configure({
            parent: '#cfluid',
            trickleRate: 0.02,
            trickleSpeed: 300
        });
        NProgress.start();

        //start tabled
        $('#dataTable').DataTable();

        //image handler
        //add image to image after select
        document.getElementById('ImageFile').onchange = function (evt) {
            var tgt = evt.target || window.event.srcElement,
                files = tgt.files;

            // FileReader support
            if (FileReader && files && files.length) {
                var fr = new FileReader();
                fr.onload = function () {
                    document.getElementById('imgSource').src = fr.result;
                }
                fr.readAsDataURL(files[0]);
            }
        }
    } catch (error) {
        //console.error('Data table init failure');
    }
});

//generate colors
function generateColor() {
    var colorArray = [];
    for (var i = 0; i < 30; i++) {
        var colorGen = getRandomColor();
        colorArray.push(colorGen);
    }
    return colorArray;
}

//display main body
function displayMainBody() {
    NProgress.done();
    $('#loading').hide();
    $("#mainBody").fadeIn().removeClass('d-none');
}

//convert float to money format
function converToMoneyFormat(value) {
    var formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'ZMK'
    });
    var money = formatter.format(value);
    return money;
}

//generate random colors
function getRandomColor(num) {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

//get value of day int
function getDayValue(value) {
    var day;
    switch (value) {
        case 0:
            day = "Sun";
            break;
        case 1:
            day = "Mon";
            break;
        case 2:
            day = "Tue";
            break;
        case 3:
            day = "Wed";
            break;
        case 4:
            day = "Thur";
            break;
        case 5:
            day = "Fri";
            break;
        case 6:
            day = "Sat";
    }
    return day;
}

//get status color
function getStatusColor(value) {
    var status;
    switch (value) {
        case "Active":
            status = "text-success";
            break;
        case "Deactivated":
            status = "text-danger";
            break;
        case "On Hold":
            status = "text-warning";
            break;
    }
    return status;
}

//get date from timestamp
function convertTimeStampToDate(value) {
    var data = new Date(value);
    var utc = data.toUTCString();
    return newData = utc.slice(8, 12) + utc.slice(5, 8) + utc.slice(17, 22);
}

function spinnerHandler(id) {
    var element = document.getElementById(id);
    var isHidden = element.classList.contains("d-none");
    if (isHidden) {
        $(`#${id}`).removeClass("d-none");
    } else {
        $(`#${id}`).addClass("d-none");
    }
}

function getDateValue(value) {
    var now = new Date(value);
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    return today;
}

//silmulates async call
async function simulateAsycCall() {
    await new Promise(r => setTimeout(r, 1000));
}

//Send mail
async function sendMail(email) {
    await makeGetRequest(`/Views/Admins/AdminsCreation?handler=SendMail&email=${email}`).then(async response => {
        console.log(response);
        if (response != null) {
            if (response.error) {
                alertify.error("Error while sending email")
            } else {
                alertify.success("Mail Sent")
            }
        }
    });
}

//get value from url
function getUrlValue(url, value) {
    var url = new URL(url);
    var c = url.searchParams.get(value);
    return c;
}

//set cookies
function setCookie(cname, cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    let expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

//get cookie
function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

//deletes all set cookies
function deleteAllCookies() {
    var cookies = document.cookie.split(";");
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i].trim();
        var eqPos = cookie.indexOf("=");
        var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
    }
    document.cookie.split(";").forEach(function (c) { document.cookie = c.replace(/^ +/, "").replace(/=.*/, "=;expires=" + new Date().toUTCString() + ";path=/"); });
}

//get select value 
function getSelectValue(id, value) {
    try {
        var list = document.getElementById(id).options;
        for (var i = 0; i < list.length; i++) {
            if (list[i].text.includes(value.trim())) {
                list[i].selected = true;
            }
        }
    } catch (error) {
        //console.error(error);
    }

}

//make get requests
async function makeGetRequest(url) {
    var headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Accept', 'application/json');
    headers.append('Access-Control-Allow-Origin', '*');

    var requestOptions = {
        method: 'GET',
        headers: headers,
        redirect: 'follow'
    };

    return await fetch(url, requestOptions)
        .then(response => response.json())
        .then(result => {
            return result;
        })
        .catch(error => console.error(`ERROR on GetRequest \nURL: ${url} \nMessage: ${error}`));
}

//make post call
async function makePostRequest(body, url) {
    var headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Accept', 'application/json');

    var requestOptions = {
        method: 'POST',
        body: JSON.stringify(body),
        headers: headers,
        redirect: 'follow'
    };

    return await fetch(url, requestOptions)
        .then(response => response.json())
        .then(result => { return result; })
        .catch(error => console.error(`ERROR on PostRequest \nURL: ${url} \nMessage: ${error}`));
}

async function makePostFormRequest(body, url) {
    var headers = new Headers();
    headers.append('Content-Type', 'multipart/form-data');

    var requestOptions = {
        method: 'POST',
        body: body
    };

    return await fetch(url, requestOptions)
        .then(response => response.json())
        .then(result => { return result; })
        .catch(error => console.error(`ERROR on makePostFormRequest \nURL: ${url} \nMessage: ${error}`));
}





