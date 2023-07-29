import React from 'react';

const UnityGame = () => {
  const gameUrl = 'https://danneruu.github.io/Aulify/';

  return (
    <div style={{ width: '100%', height: '650px', position: 'relative', overflow: 'hidden' }}>
      <iframe
        title="Unity WebGL Game"
        src={gameUrl}
        style={{
          width: '100%',
          height: '800px ',
          border: 'none',
          position: 'absolute',
          top: 0,
          left: 0
        }}
      ></iframe>
    </div>
  );
};

export default UnityGame;
