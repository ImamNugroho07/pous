let map;
function initMap() {
    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: -34.397, lng: 150.644 },
        zoom: 8,
    });
}

window.initMap = initMap;

const panorama = new PANOLENS.ImagePanorama("/images/WhatsApp Image 2022-08-27 at 18.00.43.jpeg");
const container = document.querySelector(".img-box");
const viewer = new PANOLENS.Viewer({
    container: container,
        autoHideInfospot: false
    });
viewer.add(panorama);

var infospot = new PANOLENS.Infospot(100, PANOLENS.DataImage.Info, true);
panorama.addEventListener("click", function (e) {
    if (e.intersects.length > 0) return;
    const a = viewer.raycaster.intersectObject(viewer.panorama, true)[0].point;
    infospot.position.set(-a.x, a.y, a.z);
    panorama.add(infospot);
    $(".img-box").trigger('click');

    alert("x = " + -a.x + " y = "+a.y + " z = "+a.z);


    //infospot.addHoverElement(document.getElementById('pp'), -20);
    
});
function close() {
    alert("hahahahah")
}