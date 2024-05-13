import { FC, useState } from "react";
import { IEmployee } from "../../types";
import './index.css'
import EmployeeModal from "../EmployeeModal";

interface ITableProps {
  employees: IEmployee[],
  deleteEmployee: (employee: IEmployee) => void
  statusListTitle: string,
  divisionId: number | undefined
  updateDataPage: () => void

}
const Table: FC<ITableProps> = ({ employees, deleteEmployee, statusListTitle, divisionId, updateDataPage }) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedEmployee, SetSelectedEmpoyee] = useState<IEmployee>();
  const [titleModalString, setTitleModalString] = useState("");

  return (
    <>
      <div className="tableEmployee">
        <h1>Работники</h1>
        <label>{statusListTitle}</label>
        <table>
          <thead>
            <tr>
              <td>Имя</td>
              <td>День рождения</td>
              <td>Действие</td>
            </tr>
          </thead>
          <tbody>

            {employees?.map(
              employee =>
                <tr key={employee.id}>
                  <td>{employee.secondName} {employee.firstName} {employee.lastName}</td>
                  <td>{new Date(employee.birthDay).toLocaleDateString()}</td>
                  <td>
                    <span>
                      <button onClick={() => deleteEmployee(employee)} >Удалить</button>
                      <button onClick={() => {
                        setIsModalOpen(true);
                        SetSelectedEmpoyee(employee);
                        setTitleModalString("Редактировать работника");
                      }
                      } >Редактировать</button>
                    </span>
                  </td>
                </tr>
            )}

          </tbody>
        </table>
        <button onClick={() => {
          setIsModalOpen(true);
          SetSelectedEmpoyee(undefined);
          setTitleModalString("Добавить работника");
        }
        }>Добавить работника</button>

      </div>
      <EmployeeModal
        employee={selectedEmployee}
        title={titleModalString}
        open={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        divisionId={divisionId}
        updateDataPage={updateDataPage}
      />

    </>
  )
}
export default Table;