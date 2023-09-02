function iniciarScene() {
    const scene = new THREE.Scene();
    scene.background = 0xFFFFFF; // White

    const camera = new THREE.PerspectiveCamera(75, window.innerWidth /
        window.innerHeight, 0.1, 1000);
    camera.position.z = 5;

    const renderer = new THREE.WebGLRenderer();
    renderer.setSize(window.innerWidth, window.innerHeight);
    renderer.setClearColor(0xff0000, 1);
    document.body.appendChild(renderer.domElement);

    const geometry = new THREE.BoxGeometry(50, 50, 50);
    const material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
    const cube = new THREE.Mesh(geometry, material);
    scene.add(cube);

    var light = new THREE.PointLight(0xffffff, 1, 100);
    scene.add(light);

    requestAnimationFrame(render);

    function render() {
        renderer.render(scene, camera);

        cube.rotation.x += 0.01;
        cube.rotation.y += 0.01;

        requestAnimationFrame(render);
    }
};







$(document).ready(function () {
    
});