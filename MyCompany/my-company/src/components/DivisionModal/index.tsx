import React, { ChangeEvent, FC, useEffect, useRef, useState } from 'react';
import './index.css';
import { IDivision, Post } from '../../types';
import { divisionControllerApi } from '../../api-service';
import Modal from '../Modal';
interface IModalProps {
  division: IDivision | undefined,
  open: boolean,
  title: string,
  onClose: () => void,
  updateDataPage: () => void

}
const DivisionModal: FC<IModalProps> = ({ open, title, onClose, division, updateDataPage }) => {
  const nameInput = useRef<HTMLInputElement>(null);
  const descriptionInput = useRef<HTMLTextAreaElement>(null);
  const divisionId = useRef<HTMLSelectElement>(null);
  const [divisions, setDivisions] = useState<IDivision[]>([]);
  const [selectedDivision, setSelectedDivision] = useState(division?.parentDivisionId);
  const selectDivison = (value: ChangeEvent<HTMLSelectElement>) => {
    setSelectedDivision(Number(value.target.value))
  }
  useEffect(() => {
    if (open) {
      const uri = division ? `/get-flat?checkDivisionId=${division.id}` : '/get-flat'
      divisionControllerApi.get<Post>(uri).then(response => setDivisions(response.data.data))
    }
  }, [open, division])
  const Confirm = (form: React.FormEvent<HTMLFormElement>) => {
    form.preventDefault()
    onClose()
    if (division === undefined) {
      //Создание
      const createDivision: IDivision = {
        name: nameInput.current?.value ?? " ",
        description: descriptionInput?.current?.value ?? " ",
        parentDivisionId: divisionId.current?.value === 'Главный' ? null : Number(divisionId.current?.value),
        divisions: null,
        id: 0
      }
      divisionControllerApi.post('', createDivision).then(() => updateDataPage())
    } else {
      //Обновление
      const updateDivision: IDivision = {
        ...division,
        name: nameInput.current?.value ?? " ",
        description: descriptionInput?.current?.value ?? " ",
        parentDivisionId: divisionId.current?.value === 'Главный' ? null : Number(divisionId.current?.value)
      }
      divisionControllerApi.put('', updateDivision).then(() => updateDataPage())
    }
  }
  return (
    <Modal
      title={title}
      open={open}
      onClose={onClose}
    >
      <div className="inputlist">
        <form className='formListElem' onSubmit={Confirm}>
          <input className='inputListElem'
            ref={nameInput}
            name='nameinput'
            defaultValue={division?.name ?? ''}
            placeholder='Название'
            required
            minLength={3}
            maxLength={50}
          />
          <textarea className='descriptiontextarea'
            ref={descriptionInput}
            defaultValue={division?.description ?? ''}
            placeholder='Описание'
            maxLength={150}
          />
          <select
            ref={divisionId}
            className="inputListElem"
            onChange={selectDivison}
            value={selectedDivision ?? undefined} >
            <option >Главный</option>
            {divisions.map(division =>
              <option
                key={division.id}
                value={division.id}>
                {division.name}
              </option>
            )}
          </select>
          <button type="submit">Сохранить</button>
        </form>
      </div>
    </Modal >
  )
}
export default DivisionModal;