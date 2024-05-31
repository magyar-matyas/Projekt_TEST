const canvas = document.getElementById("jatekter");
const ctx = canvas.getContext("2d");
const squareSize = 50;
const carImage = new Image();
carImage.src = "/kepek/rallycar_asset_player.png";
let irány = "down";
const tüskeImage = new Image();
tüskeImage.src = "/kepek/tüske.png";

const matrix = [
  [1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1],
  [1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 4, 0, 4, 0, 1, 0, 0, 0, 1],
  [1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1],
  [1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1],
  [1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1],
  [1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1],
  [1, 1, 1, 0, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
  [1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1],
  [1, 0, 1, 4, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 4, 1],
  [1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1],
  [1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1],
  [1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1],
  [1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1],
  [1, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1],
  [1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1],
  [1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1],
  [1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
];

carImage.onload = () => {
  matrixKiír();
};

function matrixKiír() {
  ctx.clearRect(0, 0, canvas.width, canvas.height);
  matrix.forEach((row, rowIndex) => {
    row.forEach((value, colIndex) => {
      switch (value) {
        case 1:
          ctx.fillStyle = "black";
          ctx.fillRect(
            colIndex * squareSize,
            rowIndex * squareSize,
            squareSize,
            squareSize
          );
          break;
        case 0:
          ctx.fillStyle = "white";
          ctx.fillRect(
            colIndex * squareSize,
            rowIndex * squareSize,
            squareSize,
            squareSize
          );
          break;
        case 2:
          ctx.fillStyle = "white";
          ctx.fillRect(
            colIndex * squareSize,
            rowIndex * squareSize,
            squareSize,
            squareSize
          );
          drawRotatedCar(
            colIndex * squareSize + squareSize / 2,
            rowIndex * squareSize + squareSize / 2,
            irány
          );
          break;
        case 3:
          ctx.fillStyle = "blue";
          ctx.fillRect(
            colIndex * squareSize,
            rowIndex * squareSize,
            squareSize,
            squareSize
          );
          break;
        case 4:
          ctx.fillStyle = "white";
          ctx.fillRect(
            colIndex * squareSize,
            rowIndex * squareSize,
            squareSize,
            squareSize
          );
          break;
        case 5:
          ctx.fillStyle = "white";
          ctx.fillRect(
            colIndex * squareSize,
            rowIndex * squareSize,
            squareSize,
            squareSize
          );
          ctx.drawImage(
            tüskeImage,
            colIndex * squareSize,
            rowIndex * squareSize,
            squareSize,
            squareSize
          );
          break;
      }
    });
  });
}

function drawRotatedCar(x, y, irány) {
  ctx.save();
  ctx.translate(x, y);
  switch (irány) {
    case "up":
      break;
    case "down":
      ctx.rotate(Math.PI);
      break;
    case "left":
      ctx.rotate(-Math.PI / 2);
      break;
    case "right":
      ctx.rotate(Math.PI / 2);
      break;
  }
  ctx.drawImage(
    carImage,
    -squareSize / 2,
    -squareSize / 2,
    squareSize,
    squareSize
  );
  ctx.restore();
}

function KocsiMegtalal() {
  for (let rowIndex = 0; rowIndex < matrix.length; rowIndex++) {
    for (let colIndex = 0; colIndex < matrix[rowIndex].length; colIndex++) {
      if (matrix[rowIndex][colIndex] === 2) {
        return { rowIndex, colIndex };
      }
    }
  }
}

function KocsiMozgat(dx, dy) {
  const { rowIndex, colIndex } = KocsiMegtalal();
  const newRowIndex = rowIndex + dy;
  const newColIndex = colIndex + dx;

  if (
    newRowIndex >= 0 &&
    newRowIndex < matrix.length &&
    newColIndex >= 0 &&
    newColIndex < matrix[0].length &&
    matrix[newRowIndex][newColIndex] !== 1
  ) {
    if (KocsiCélbaÉr(newRowIndex, newColIndex)) {
      matrix[rowIndex][colIndex] = 0;
      matrix[newRowIndex][newColIndex] = 2;
      matrixKiír();
      showPopup("Gratulálunk, célba értél!");
      return;
    }

    if (matrix[newRowIndex][newColIndex] === 4) {
      matrix[newRowIndex][newColIndex] = 5;
      matrixKiír();
      showPopup("Meghaltál egy tüskében!");
      return;
    }

    matrix[rowIndex][colIndex] = 0;
    matrix[newRowIndex][newColIndex] = 2;

    if (dx === 0 && dy === -1) irány = "up";
    if (dx === 0 && dy === 1) irány = "down";
    if (dx === -1 && dy === 0) irány = "left";
    if (dx === 1 && dy === 0) irány = "right";

    matrixKiír();
  }
}

function KocsiCélbaÉr(rowIndex, colIndex) {
  return matrix[rowIndex][colIndex] === 3;
}

function showPopup(message) {
  const popup = document.getElementById("popup");
  const popupMessage = document.getElementById("popup-message");
  popupMessage.textContent = message;
  popup.classList.add("show");

  document.removeEventListener("keydown", handleKeyDown);

  const returnButton = document.getElementById("return-button");
  returnButton.addEventListener("click", returnToHomepage);

  const playAgainButton = document.getElementById("game-again-button");
  playAgainButton.addEventListener("click", playAgain);
}

function handleKeyDown(event) {
  switch (event.key) {
    case "w":
      KocsiMozgat(0, -1);
      break;
    case "a":
      KocsiMozgat(-1, 0);
      break;
    case "s":
      KocsiMozgat(0, 1);
      break;
    case "d":
      KocsiMozgat(1, 0);
      break;
  }
}

function playAgain() {
  window.location.reload();
}

function returnToHomepage() {
  window.location.href = "fooldal.html";
}

document.addEventListener("keydown", handleKeyDown);
