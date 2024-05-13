import { FC, useEffect, useRef, useState } from "react";
import './index.css'
import { IDivision, IEmployee, Post } from "../../types";
import { divisionControllerApi, employeeControllerApi } from "../../api-service";
import Modal from "../Modal";

interface IModalProps {
  employee?: IEmployee | undefined,
  open: boolean,
  title: string,
  onClose: () => void,
  divisionId: number | undefined,
  updateDataPage: () => void

}
const formatDate = (date: string | undefined): string => {
  if (!date) {
    const datenow = new Date(Date.now())
    datenow.setFullYear(datenow.getFullYear() - 18)
    return formatDate(datenow.toString())
  }
  const formDate = new Date(date)
  let tmp;
  return (
    (tmp = formDate.getFullYear()) < 10 ? '0' + tmp : tmp)
    + '-' + ((tmp = formDate.getMonth() + 1) < 10 ? '0' + tmp : tmp)
    + '-' + ((tmp = formDate.getDate()) < 10 ? '0' + tmp : tmp);
}

const EmployeeModal: FC<IModalProps> = ({ employee, open, title, onClose, divisionId, updateDataPage }) => {
  const [divisions, setDivisions] = useState<IDivision[]>([]);

  useEffect(() => {
    if (open)
      divisionControllerApi.get<Post>('/get-flat').then(response => setDivisions(response.data.data))
  }, [open])

  const firstNameInput = useRef<HTMLInputElement>(null);
  const secondNameInput = useRef<HTMLInputElement>(null);
  const lastNameInput = useRef<HTMLInputElement>(null);
  const genderSelect = useRef<HTMLSelectElement>(null);
  const driverLicenseCheckBox = useRef<HTMLInputElement>(null);
  const dateBirthDay = useRef<HTMLInputElement>(null);
  const positionInput = useRef<HTMLInputElement>(null);
  const divisionSelected = useRef<HTMLSelectElement>(null);

  const Confirm = (form: React.FormEvent<HTMLFormElement>) => {
    form.preventDefault()
    onClose()
    if (employee === undefined) {
      //Создание
      const createEmployee: IEmployee = {
        id: 0,
        firstName: firstNameInput.current?.value ?? " ",
        secondName: secondNameInput.current?.value ?? " ",
        lastName: lastNameInput.current?.value ?? " ",
        gender: genderSelect.current?.value,
        driverLicense: Boolean(driverLicenseCheckBox.current?.checked),
        birthDay: dateBirthDay.current?.value ?? "",
        position: positionInput.current?.value ?? "",
        divisionid: Number(divisionSelected.current?.value)
      }
      employeeControllerApi.post('', createEmployee).then(() => updateDataPage())
    } else {
      //обновление
      const updateEmployee: IEmployee = {
        ...employee,
        firstName: firstNameInput.current?.value ?? " ",
        secondName: secondNameInput.current?.value ?? " ",
        lastName: lastNameInput.current?.value ?? " ",
        gender: genderSelect.current?.value,
        driverLicense: Boolean(driverLicenseCheckBox.current?.checked),
        birthDay: dateBirthDay.current?.value ?? "",
        position: positionInput.current?.value ?? "",

      }
      employeeControllerApi.put('', updateEmployee).then(() => updateDataPage())
    }


  }

  return (
    <>
      <Modal
        title={title}
        open={open}
        onClose={onClose}
      >
        <form onSubmit={Confirm}>
          <div className="inputlist">
            <input className="inputListElem"
              required ref={firstNameInput}
              defaultValue={employee?.firstName ?? ""}
              placeholder="Имя"
              maxLength={50}
            ></input>

            <input className="inputListElem"
              required ref={secondNameInput}
              defaultValue={employee?.secondName ?? ""}
              placeholder="Фамилия"
              maxLength={50}
            ></input>

            <input className="inputListElem"
              required ref={lastNameInput}
              defaultValue={employee?.lastName ?? ""}
              placeholder="Отчество"
              maxLength={50}
            ></input>

            <label >Дата рождения:</label>
            <input className="inputListElem"
              required
              type="date"
              ref={dateBirthDay}
              max={formatDate(undefined)}
              defaultValue={formatDate(employee?.birthDay)}
            />
            <input className="inputListElem"
              defaultValue={employee?.position ?? ""}
              ref={positionInput}
              placeholder="Должность"
              maxLength={50}
            ></input>


            <select className="inputListElem"
              defaultValue={employee?.gender}
              ref={genderSelect}
            >
              <option disabled selected>Пол</option>
              <option value='Жен'>Жен</option>
              <option value='Муж'>Муж</option>
            </select>
            <span>
              <label className="inputListElem"> Водительское удостоверение
                <input type="checkbox"
                  defaultChecked={!!employee?.driverLicense}
                  ref={driverLicenseCheckBox}
                ></input>
              </label>
            </span>
            <select required
              className="inputListElem"
              defaultValue={divisionId}
              ref={divisionSelected}
            >
              {divisions.map(division =>
                <option
                  key={division.id}
                  value={division.id}>
                  {division.name}
                </option>
              )}
            </select>
            <button type="submit">Сохранить</button>
          </div>
        </form>

      </Modal>
    </>
  )

}
export default EmployeeModal;