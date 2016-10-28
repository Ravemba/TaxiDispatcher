﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Driver
{
	/// <summary>
	/// Паспорт
	/// </summary>
	public string Pasport
	{
		get;
		set;
	}

	/// <summary>
	/// Фамилия
	/// </summary>
	public string Surname
	{
		get;
		set;
	}

	/// <summary>
	/// Имя
	/// </summary>
	public string Name
	{
		get;
		set;
	}

	/// <summary>
	/// Отчество
	/// </summary>
	public string Patronymic
	{
		get;
		set;
	}

	/// <summary>
	/// Псевдоним
	/// </summary>
	public string Nickname
	{
		get;
		set;
	}

	/// <summary>
	/// Позывной
	/// </summary>
	public string Callsign
	{
		get;
		set;
	}

	/// <summary>
	/// Адреса
	/// </summary>
	public IEnumerable<Address> Addresses
	{
		get;
		set;
	}

	/// <summary>
	/// Заметки
	/// </summary>
	public string Notes
	{
		get;
		set;
	}

	/// <summary>
	/// Диспетчер
	/// </summary>
	public DispatcherType DispatcherType
	{
		get;
		set;
	}

	/// <summary>
	/// GPRS
	/// </summary>
	public bool GPRS
	{
		get;
		set;
	}

	/// <summary>
	/// Рация
	/// </summary>
	public bool RadioSet
	{
		get;
		set;
	}

	/// <summary>
	/// День рожденья
	/// </summary>
	public DateTime DateOfBirthday
	{
		get;
		set;
	}

	/// <summary>
	/// Принят наработу
	/// </summary>
	public DateTime Adopted
	{
		get;
		set;
	}

	/// <summary>
	/// Уволен
	/// </summary>
	public DateTime Fired
	{
		get;
		set;
	}

	/// <summary>
	/// Пароль
	/// </summary>
	public string Password
	{
		get;
		set;
	}

	/// <summary>
	/// Сдал залог
	/// </summary>
	public decimal PassedPledge
	{
		get;
		set;
	}

	/// <summary>
	/// Долг залог
	/// </summary>
	public decimal DebtPledge
	{
		get;
		set;
	}

	/// <summary>
	/// Абонплата с заказа, гр
	/// </summary>
	public DriverFeeToOrder DriverFeeToOrder
	{
		get;
		set;
	}

	/// <summary>
	/// Номер телефона
	/// </summary>
	public IEnumerable<String> Phones
	{
		get;
		set;
	}

	/// <summary>
	/// По совместительству
	/// </summary>
	public DriverConcurrently DriverConcurrently
	{
		get;
		set;
	}

	public DriverFeeMissingOrder DriverFeeMissingOrder
	{
		get;
		set;
	}

	public virtual DriverLicense DriverLicense
	{
		get;
		set;
	}

	public virtual DriverPersonalCommission DriverPersonalCommission
	{
		get;
		set;
	}

	public virtual DriverAccessSetting DriverAccessSetting
	{
		get;
		set;
	}

	public virtual DriverAdditionalSetting DriverAdditionalSetting
	{
		get;
		set;
	}

	public virtual IEnumerable<Address> Address
	{
		get;
		set;
	}

}

