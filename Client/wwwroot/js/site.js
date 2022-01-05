// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function htmlFunction() {
    var javascriptData = document.getElementById('button-javascript');
    javascriptData.classList.remove('active');
    var htmlData = document.getElementById('button-html');
    htmlData.classList.add('active');
    var cssData = document.getElementById('button-css');
    cssData.classList.remove('active');

    document.getElementsByClassName('title')[0].innerHTML = 'The Basic Languange of the web : HTML';

    document.getElementsByClassName('date-post')[0].innerHTML = 'Posted by <b>Mozilla</b> on Monday, June 21st 2027';
    document.getElementById('image').src = '/image/pic_html.jpeg';
    document.getElementsByClassName('topic')[0].innerHTML = 'What is HTML?';
    document.getElementsByClassName('body-topic')[0].innerHTML = 'HTML (HyperText Markup Language) is the most basic building block of the Web. It defines the meaning and structure of web content. Other technologies besides HTML are generally used to describe a web page appearance/presentation (CSS) or functionality / behavior(JavaScript).';
}

function cssFunction() {
    var javascriptData = document.getElementById('button-javascript');
    javascriptData.classList.remove('active');
    var htmlData = document.getElementById('button-html');
    htmlData.classList.remove('active');
    var cssData = document.getElementById('button-css');
    cssData.classList.add('active');

    document.getElementsByClassName('title')[0].innerHTML = 'The Basic Languange of the web : CSS';
  
    document.getElementsByClassName('date-post')[0].innerHTML = 'Posted by <b>Mozilla</b> on Saturday, June 24st 2027';
    document.getElementById('image').src = '/image/pic_css.png';
    document.getElementsByClassName('topic')[0].innerHTML = 'What is CSS?';
    document.getElementsByClassName('body-topic')[0].innerHTML = 'Cascading Style Sheets (CSS) is a stylesheet language used to describe the presentation of a document written in HTML or XML (including XML dialects such as SVG, MathML or XHTML). CSS describes how elements should be rendered on screen, on paper, in speech, or on other media.';
}


function javascriptFunction() {
    var javascriptData = document.getElementById('button-javascript');
    javascriptData.classList.add('active');
    var htmlData = document.getElementById('button-html');
    htmlData.classList.remove('active');
    var cssData = document.getElementById('button-css');
    cssData.classList.remove('active');

    document.getElementsByClassName('title')[0].innerHTML = 'The Basic Languange of the web : JavaScript';

    document.getElementsByClassName('date-post')[0].innerHTML = 'Posted by <b>Mozilla</b> on Sunday, June 28st 2027';
    document.getElementById('image').src = '/image/pic_javascript.jpg';
    document.getElementsByClassName('topic')[0].innerHTML = 'What is JavaScript?';
    document.querySelector('body-topic').innerHTML = 'JavaScript (JS) is a lightweight, interpreted, or just-in-time compiled programming language with first-class functions. While it is most well-known as the scripting language for Web pages, many non-browser environments also use it, such as Node.js, Apache CouchDB and Adobe Acrobat. ';
}