import { ListItem, ListItemAvatar, Avatar, ListItemText, InputAdornment, IconButton } from "@mui/material";
import { ShoppingCartItem } from "../../../global/models/shoppingCart";
import { DeleteOutline } from "@mui/icons-material";
import MyTextInput from "../../../components/MyTextInput";
import { Form, Formik } from "formik";
import * as Yup from 'yup'

interface Props {
  item: ShoppingCartItem;
  key: string
  onRemove: () => void;
  onQuantityChange: (newQuantity: number) => void;
}

const ShoppingCartItemComponent: React.FC<Props> = ({ item, onRemove, onQuantityChange}) => {

  function getMaxQ(): number | undefined {
    if(item.quantity.unit.code === 'kg') {
      return 50
    } else {
      //TODO Ogarnąć KgPerPcs w tym casie
      return Number((50 / item.singleWeight?.value!).toFixed(0))
    }
  }

  return (
    <ListItem key={item.productId}>
      <ListItemAvatar>
          <Avatar  src={item.productImage.url} />
      </ListItemAvatar>
      <ListItemText
        primary={item.productName}
        secondary={`${Number(item.totalValue?.amount.toFixed(1))} ${item.totalValue!.currency.code} • ${item.price.amount} ${item.price.currency.code}/${item.price.unit?.code}`}
      />
      <Formik
          validationSchema={Yup.object({quantity: Yup.number()
                                                     .required('Proszę podać ilość')
                                                     .moreThan(0,"Podaj ilość większą niż 0")
                                                     .lessThan(getMaxQ()! + 1, 'Max 50 kg')})}
          initialValues={{quantity: item.quantity.value}}
          onSubmit={values => onQuantityChange(Number(values.quantity))}
          validateOnMount>
            {({ handleSubmit, isValid}) => (
              <Form className='ui form' onBlur={handleSubmit} onSubmit={handleSubmit} autoComplete='off' style={{width: '100px'}}>
                <MyTextInput 
                inputProps={{ endAdornment: <InputAdornment position="start">{item.quantity.unit.code}</InputAdornment> }} 
                placeholder={"Ilość"} 
                maxValue={getMaxQ()}
                minValue={0}
                showErrors
                type="number"
                name={"quantity"}
                />
              </Form>
            )}
        </Formik>
      <IconButton edge="end" onClick={onRemove}>
          <DeleteOutline color="primary" />
      </IconButton>
    </ListItem>
  );
}

export default ShoppingCartItemComponent;