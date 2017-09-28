var random = function (min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

var navigateToNextPage = function (url, timeout) {
    setTimeout(function () { window.location.href = url }, timeout)
}

var randomUrl = function () {
    var magicNumber = random(1, 5);
    var timeout = 10000; // parseInt((Math.random() * 10)) * 1000;
    switch (magicNumber) {
        case 1:
            navigateToNextPage('http://newrelicdemo.com/', timeout);
            break;
        case 2:
            navigateToNextPage('http://newrelicdemo.com//Home/About', timeout);
            break;
        case 3:
            navigateToNextPage('http://newrelicdemo.com//Home/Contact', timeout);
            break;
        case 4:
            navigateToNextPage('http://newrelicdemo.com/home/GetDataFromDB', timeout);
            break;
        case 5:
            navigateToNextPage('http://newrelicdemo.com/home/APIData', timeout);
            break;
        default:
            navigateToNextPage('http://newrelicdemo.com/', timeout);
    }
}();


throw "An error was happen!";