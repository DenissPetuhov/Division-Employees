import React, { Dispatch, FC, SetStateAction, useMemo, useReducer, useState } from 'react';
import { IDivision } from '../../types';
import './index.css';

import DivisionModal from '../DivisionModal';

interface IMenuItemProps {
  division: IDivision,
  setSelectDivision: Dispatch<SetStateAction<IDivision | undefined>>,
  deleteDivision: (division: IDivision) => void,
  updateDataPage: () => void


}

const MenuItem: FC<IMenuItemProps> = ({ division, setSelectDivision, deleteDivision, updateDataPage }) => {
  const [isOpen, setIsOpen] = useReducer((state) => !state, false);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const showTreeSymbol = useMemo(() => division.divisions !== null && division.divisions.length > 0, [division]);
  const treeSymbol = useMemo(() => {
    if (!showTreeSymbol)
      return null;
    return isOpen ? '▼' : '▶';
  }, [isOpen, showTreeSymbol]);

  return (
    <>
      <li className='listItem'>
        <div className='menuItem'>
          <div className='treeSymbol' onClick={() => setIsOpen()}>{treeSymbol}</div>
          <button onClick={() => setSelectDivision(division)}>{division.name}</button>
          <div className='icongroup'>
            <button onClick={() => deleteDivision(division)}>Удалить</button>
            <button onClick={() => setIsModalOpen(true)}>Редактировать</button>
          </div>
        </div>
        <ul>
          {isOpen && division.divisions?.map(child =>
          (
            <MenuItem
              key={child.id}
              division={child}
              updateDataPage={updateDataPage}
              setSelectDivision={setSelectDivision}
              deleteDivision={deleteDivision}

            />
          )
          )}
        </ul>
      </li>
      <DivisionModal
        division={division}
        title='Редактирование отдела'
        open={isModalOpen}
        updateDataPage={updateDataPage}
        onClose={() => setIsModalOpen(false)} />

    </>
  )
}
export default MenuItem;