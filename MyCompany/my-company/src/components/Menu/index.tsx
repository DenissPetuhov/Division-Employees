import { Dispatch, FC, SetStateAction, useState } from 'react';
import { IDivision } from '../../types';
import MenuItem from '../MenuItem';
import './index.css';
import DivisionModal from '../DivisionModal';

interface IMenuProps {
  divisions: IDivision[];
  setSelectedDivision: Dispatch<SetStateAction<IDivision | undefined>>
  deleteDivision: (division: IDivision) => void,
  updateDataPage: () => void

}

const Menu: FC<IMenuProps> = ({ divisions, setSelectedDivision, deleteDivision, updateDataPage }) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  return (
    <>
      <div className='menu'>
        <h1>Отделы</h1>
        <ul>
          {divisions?.map(
            division =>
              <MenuItem
                key={division.id}
                division={division}
                updateDataPage={updateDataPage}
                setSelectDivision={setSelectedDivision}
                deleteDivision={deleteDivision}
              />
          )}
        </ul>
        <div className='addDivisionButton'>
          <button onClick={() => {
            setIsModalOpen(true);
          }}>Добавить отдел</button>
        </div>
      </div>
      <DivisionModal
        title='Редактирование отдела'
        open={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        division={undefined}
        updateDataPage={updateDataPage} />
    </>
  )
}
export default Menu;