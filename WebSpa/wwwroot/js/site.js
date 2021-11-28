function processAjaxData(response, urlPath){
    document.getElementById("app").innerHTML = response.Html;
    document.title = response.Title;
    const data={"Html":response.Html,"Title":response.Title};
    console.log("data");
    console.log(data);
    window.history.pushState(data,"", urlPath);
}

window.onpopstate = function(e){
    if(e.state){
        document.getElementById("app").innerHTML = e.state.Html;
        document.title = e.state.Title;
    }
};

addEventListener("load",function(){
    var links= document.getElementsByTagName("a");
    const headers = new Headers();
    headers.append('X-Requested-With', 'XMLHttpRequest');
    for (var i=0;i<links.length;i++){
        links[i].addEventListener("click",function(e){
            let href=e.target.href;
            NProgress.start();
            fetch(href,{method:"GET",headers}).then(res=>res.json()).then(result=>{
              processAjaxData(result,href);
              NProgress.done();
            });
            e.preventDefault();
        })
    }
});