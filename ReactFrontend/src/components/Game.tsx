
export const BOARD = Array(161).fill([]);

export const Game = () => {
  return (
    <div className="main flex">
      <div className="container">
        
        {
          BOARD.map(x => <div/>)
        }
      </div>
    </div>
  );
};