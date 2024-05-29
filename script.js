const jatekter = document.querySelector('#jatekter');
const context = jatekter.getContext('2d');
const jatekos = document.querySelector('.player');

const fal = 10;
const labirintus = [
    [0, 1, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 1, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 1, 0, 0, 0, 0, 0, 0, 0, 0],
    [0, 1, 0, 1, 1, 1, 1, 1, 0, 0],
    [0, 1, 0, 1, 0, 1, 0, 1, 0, 0],
    [0, 1, 0, 1, 0, 1, 0, 1, 0, 0],
    [0, 1, 0, 1, 0, 1, 0, 1, 0, 0],
    [0, 1, 1, 1, 1, 1, 0, 1, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 1, 0, 0],
    [0, 0, 0, 0, 0, 0, 0, 1, 1, 1],
];

function labirintusGen() {
    for (let sor = 0; sor < labirintus.length; sor++) {
        for (let oszlop = 0; oszlop < labirintus[0].length; oszlop++) {
            context.fillStyle = ["black", "white"][labirintus[sor][oszlop]];
            context.fillRect(oszlop * fal, sor * fal, fal, fal);
        }
    }
}
labirintusGen();

// const jatekos = {
//     x: 0,
//     y: 9,
//     meret: fal,
//     szin: 'kék'
// };

// const cel = {
//     x: 9,
//     y: 0,
//     meret: fal,
//     szin: 'piros'
// };

// function cel() {
//     context.fillStyle = cel.szin;
//     context.fillRect(cel.x * fal, cel.y * fal, cel.meret, cel.meret);
// }

function uresPalya() {
    context.clearRect(0, 0, jatekter.width, jatekter.height);
}

// function frissitJatek() {
//     uresPalya();
//     labirintusGen();
//     rajzolJatekos();
//     cel();
// }

function mozgatas(dx, dy) {
    const ujX = jatekos.x + dx;
    const ujY = jatekos.y + dy;

    if (ujX >= 0 && ujX < labirintus[0].length && ujY >= 0 && ujY < labirintus.length && labirintus[ujY][ujX] === 0) {
        jatekos.x = ujX;
        jatekos.y = ujY;
    }



    if (jatekos.x === cel.x && jatekos.y === cel.y) {
        alert("Gratulálok! Nyertél!");
        jatekos.x = 0;
        jatekos.y = 9;
        frissitJatek();
    }
}

// document.addEventListener('keydown', (esemeny) => {
//     switch (esemeny.key) {
//         case 'ArrowUp':
//             mozgatas(0, -1);
//             break;
//         case 'ArrowDown':
//             mozgatas(0, 1);
//             break;
//         case 'ArrowLeft':
//             mozgatas(-1, 0);
//             break;
//         case 'ArrowRight':
//             mozgatas(1, 0);
//             break;
//     }
// });

