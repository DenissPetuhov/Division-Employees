import React, { useEffect, useMemo, useState } from 'react';
import './App.css';
import Menu from './components/Menu';
import Table from './components/Table';
import { IDivision, IEmployee, Post } from './types';
import { divisionControllerApi, employeeControllerApi } from './api-service';

function App() {
  const [divisions, setDivisions] = useState<IDivision[]>([]);
  const [employees, setEmployes] = useState<IEmployee[]>([]);
  const [selectedDivision, setSeleсtedDivision] = useState<IDivision>();

  const lableListText = useMemo(() => employees?.length < 1 ? "Работников в списке нет" : "", [employees?.length]);

  const deleteDivision = async (division: IDivision) => {
    if (window.confirm(`Удалить отдел ${division.name}?`)) {
      const response = await divisionControllerApi.delete<Post>(`?id=${division.id}`);
      if (response.data.isSuccess) {
        alert("Удаление успешно")
        updateDataPage()
        if (selectedDivision?.id && division.id === selectedDivision.id)
          setSeleсtedDivision(undefined)
      }
    }
  }

  const deleteEmployee = async (employee: IEmployee) => {
    if (window.confirm(`Удалить работнка ${employee.firstName} ${employee.lastName}`)) {
      const response = await employeeControllerApi.delete(`?id=${employee.id}`)
      if (response.data.isSuccess) {
        alert("Удаление успешно")
        updateDataPage()
      }
    }
  }

  const updateDataPage = () => {
    divisionControllerApi.get<Post>('').then(response => setDivisions(response.data.data));
    if (selectedDivision)
      employeeControllerApi.get<Post>(`?id=${selectedDivision?.id}`).then(response => setEmployes(response.data.data));

  }

  useEffect(() => {
    if (selectedDivision)
      employeeControllerApi.get<Post>(`?id=${selectedDivision?.id}`).then(response => setEmployes(response.data.data));
  }, [selectedDivision, employees.length])

  useEffect(() => {
    divisionControllerApi.get<Post>('').then(response => setDivisions(response.data.data));
  }, []);

  return (
    <>
      <h1 className='mainHeader' >Моя Компания</h1>
      <div className='root'>
        <Menu
          updateDataPage={updateDataPage}
          divisions={divisions}
          setSelectedDivision={setSeleсtedDivision}
          deleteDivision={deleteDivision}
        />
        <Table
          updateDataPage={updateDataPage}
          employees={employees}
          deleteEmployee={deleteEmployee}
          statusListTitle={lableListText}
          divisionId={selectedDivision?.id}
        />
      </div>
    </>
  )

}

export default App;
