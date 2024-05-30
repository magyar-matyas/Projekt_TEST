const canvas = document.getElementById('jatekter');
const jatekos = document.getElementById('jatekos');
const ctx = canvas.getContext('2d');

const matrix = [
    [1, 1, 1, 3, 1, 1, 1, 1, 1, 1],
    [1, 0, 0, 0, 1, 0, 0, 0, 0, 1],
    [1, 0, 1, 1, 1, 0, 1, 1, 0, 1],
    [1, 0, 1, 1, 1, 0, 1, 1, 0, 1],
    [1, 0, 0, 0, 1, 0, 0, 1, 0, 1],
    [1, 1, 1, 0, 1, 1, 0, 1, 0, 1],
    [1, 0, 0, 0, 1, 1, 0, 1, 0, 1],
    [1, 0, 1, 1, 1, 1, 0, 1, 0, 1],
    [1, 0, 0, 0, 0, 0, 0, 1, 0, 1],
    [1, 1, 1, 1, 1, 1, 1, 1, 2, 1]
];

const squareSize = 50; // Each square is 50x50 pixels


matrix.forEach((row, rowIndex) => {
    row.forEach((value, colIndex) => {
        switch(value) {
            case 1:
                ctx.fillStyle = 'black';
                break;
            case 0:
                ctx.fillStyle = 'white';
                break;
            case 2:
                ctx.fillStyle = "red";
                break;
            case 3:
                ctx.fillStyle = 'blue';
                break;
            default:
                ctx.fillStyle = 'white';
        }
        ctx.fillRect(colIndex * squareSize, rowIndex * squareSize, squareSize, squareSize);
    });
});




function uresPalya() {
    context.clearRect(0, 0, jatekter.width, jatekter.height);
}

function findRedTile() {
    for (let rowIndex = 0; rowIndex < matrix.length; rowIndex++) {
        for (let colIndex = 0; colIndex < matrix[rowIndex].length; colIndex++) {
            if (matrix[rowIndex][colIndex] === 2) {
                return { rowIndex, colIndex };
            }
        }
    }
}

function moveRedTile(dx, dy) {
    const { rowIndex, colIndex } = findRedTile();
    const newRow = rowIndex + dy;
    const newCol = colIndex + dx;

    if (newRow >= 0 && newRow < matrix.length && newCol >= 0 && newCol < matrix[0].length && matrix[newRow][newCol] === 0) {
        matrix[rowIndex][colIndex] = 0;
        matrix[newRow][newCol] = 2;
        drawMatrix();
    }
}

document.addEventListener('keydown', (event) => {
    switch(event.key) {
        case 'w':
            moveRedTile(0, -1);
            break;
        case 'a':
            moveRedTile(-1, 0);
            break;
        case 's':
            moveRedTile(0, 1);
            break;
        case 'd':
            moveRedTile(1, 0);
            break;
    }
});

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

