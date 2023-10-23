import React, {Component, useEffect, useState} from "react";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../global/stores/store";
import {Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography} from "@mui/material";
import {PriceList} from "../../../global/models/priceList";
import PriceListListItem from "./PriceListListItem";
import LoadingComponent from "../../../components/LoadingComponent";
import LoadingTableRow from "../../../components/LoadingTableRow";

function PriceListList() {
    const {adminPriceListStore} = useStore()

    useEffect(() => {
        adminPriceListStore.loadPriceLists()
    }, [adminPriceListStore.priceListsRegistry])

    return (
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell width={'33%'} style={{textAlign: 'center'}} >
                            <Typography variant='h5'><strong>Nazwa Cennika</strong></Typography>
                        </TableCell>
                        <TableCell width={'33%'} style={{textAlign: 'center'}} >
                            <Typography variant='h5'><strong>Kontrahent</strong></Typography>
                        </TableCell>
                        <TableCell width={'33%'} style={{textAlign: 'center'}}>
                            <Typography variant='h5'><strong>Akcje</strong></Typography>
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {
                        adminPriceListStore.loading ?
                        <LoadingTableRow rows={4} cells={3} />
                        :
                        adminPriceListStore.priceLists.map((pl) => (
                            <PriceListListItem key={pl.id} priceList={pl}/>
                        ))
                    }
                </TableBody>
            </Table>
        </TableContainer>
    )
}

export default observer(PriceListList)