import {IconButton, Stack, TableCell, TableRow, Typography} from "@mui/material";
import {
    CalendarMonth,
    Delete,
    Edit,
    EditCalendar, EventAvailable, EventBusy,
    Https,
    NoEncryption,
    PersonAdd,
    PersonRemove
} from "@mui/icons-material";
import {observer} from "mobx-react-lite";
import {ShopWorkingDay, StationaryShop} from "../../../../global/models/shop";
import {useStore} from "../../../../global/stores/store";
import ConfirmModal from "../../../../components/ConfirmModal";
import {TimeRange, WeekDay} from "../../../../global/models/common";
import {StationaryShopWeekDayUpdateModal} from "../modals/stationaryShop/StationaryShopWeekDayUpdateModal";
import {StationaryShopWorkScheduleInitModal} from "../modals/stationaryShop/StationaryShopWorkScheduleInitModal";
import {StationaryShopWeekDayDisableModal} from "../modals/stationaryShop/StationaryShopWeekDayDisableModal";
import {StationaryShopWeekDayEnableModal} from "../modals/stationaryShop/StationaryShopWeekDayEnableModal";

interface Props {
    shop: StationaryShop
}

function StationaryShopListItemActions({shop}: Props) {
    const {modalStore, adminShopStore} = useStore()

    const handleWorkScheduleCreate = (schedule: ShopWorkingDay[]) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setStationaryShopUpdateValues(schedule, null,null, null, null)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    const handleWeekDayUpdate = (weekDay: string, newWorkingHours: TimeRange) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setStationaryShopUpdateValues(null, {value: weekDay},newWorkingHours, null, null)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    const handleWeekDayDisable = (weekDay: WeekDay) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setStationaryShopUpdateValues(null, null,null, weekDay, null)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    const handleWeekDayEnable = (weekDay: WeekDay) => {
        adminShopStore.setShopToUpdateId(shop.id)
        adminShopStore.setStationaryShopUpdateValues(null, null,null, null, weekDay)
        modalStore.closeModal()
        adminShopStore.updateShop()
    }

    return (
        <>
            {!shop.weekSchedule.find(x => (!x.isClosed && x.openHours !== null) || (x.isClosed && x.cachedOpenHours !== null)) ?
                <IconButton style={{borderRadius: '0px', color: '#042cab', flexDirection:'column', width: '70%'}} size='medium'
                            onClick={() => modalStore.openModal(
                                <StationaryShopWorkScheduleInitModal shop={shop} onSubmit={(schedule) => handleWorkScheduleCreate(schedule)} />)}>
                    <CalendarMonth />
                    <Typography variant='caption'>Stwórz tydzień pracy</Typography>
                </IconButton>
                :
                <>
                    <IconButton style={{borderRadius: '0px', color: '#042cab', flexDirection:'column', width: '23%'}} size='medium'
                                onClick={() => modalStore.openModal(
                                    <StationaryShopWeekDayUpdateModal shop={shop}
                                                                      onChange={(weekDay, openHours) =>
                                                                          handleWeekDayUpdate(weekDay, openHours)}  />)}>
                        <EditCalendar />
                        <Typography variant='caption'>Edytuj dzień pracy</Typography>
                    </IconButton>
                    <IconButton style={{borderRadius: '0px', color: '#777777', flexDirection:'column', width: '23%'}} size='medium'
                                onClick={() => modalStore.openModal(
                                    <StationaryShopWeekDayDisableModal shop={shop}
                                                                       onSubmit={(weekDayToDisable) => handleWeekDayDisable(weekDayToDisable)} />)}
                    >
                        <EventBusy />
                        <Typography variant='caption'>Zablokuj dzień pracy</Typography>
                    </IconButton>
                    <IconButton style={{borderRadius: '0px', color: '#00949b', flexDirection:'column', width: '23%'}} size='medium'
                                onClick={() => modalStore.openModal(
                                    <StationaryShopWeekDayEnableModal shop={shop}
                                                                      onSubmit={(weekDayToEnable) => handleWeekDayEnable(weekDayToEnable)}/>)}
                                disabled={!shop.weekSchedule.find(d => d.isClosed && d.cachedOpenHours)}>
                        <EventAvailable />
                        <Typography variant='caption'>Odblokuj dzień pracy</Typography>
                    </IconButton>
                </>
            }
        </>
    )
}

export default observer(StationaryShopListItemActions)