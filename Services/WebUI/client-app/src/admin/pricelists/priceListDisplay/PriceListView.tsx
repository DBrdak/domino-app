import {LineItem, PriceList} from "../../../global/models/priceList";
import {Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography} from "@mui/material";
import LoadingTableRow from "../../../components/LoadingTableRow";
import PriceListListItem from "../priceListList/PriceListListItem";
import React, {useEffect, useState} from "react";
import PriceListLineItem from "./PriceListLineItem";
import LineItemCreateSection from "./lineItemCreation/LineItemCreateSection";
import {useStore} from "../../../global/stores/store";
import {observer} from "mobx-react-lite";

function PriceListView() {
    const {adminPriceListStore} = useStore()
    const {selectedPriceList} = adminPriceListStore
    const [priceList, setPriceList] = useState(selectedPriceList)
    const [lineItems, setLineItems] = useState<LineItem[]>([])

    useEffect(() => {
        setPriceList(selectedPriceList)
        setLineItems(selectedPriceList?.lineItems!)
    }, [selectedPriceList])

    return (
        <>
            <LineItemCreateSection priceListId={priceList!.id} />
            <TableContainer>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell width={'33%'} style={{textAlign: 'center'}} >
                                <Typography variant='h5'><strong>Produkt</strong></Typography>
                            </TableCell>
                            <TableCell width={'33%'} style={{textAlign: 'center'}} >
                                <Typography variant='h5'><strong>Cena</strong></Typography>
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
                            lineItems.map((li) => (
                                <PriceListLineItem key={li.name} lineItem={li} priceList={priceList!}/>
                            ))
                        }
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    );
}

export default observer(PriceListView)